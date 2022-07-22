using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //used for sorting priorites

public class SubGoal
{
    public Dictionary<string, int> sgoals; //sub goals will allow for complex behaviours
    public bool remove; //when agents have satisfied a goal, that goal needs to get removed, we can create a goal that will not be removed once satisfied

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public Ginventory inventory = new Ginventory();
    public WorldStates beliefs = new WorldStates();

    private GPlanner planner; //class that does the planning
    private Queue<GAction> actionQueue;
    public GAction currentAction;
    private SubGoal currentGoal;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>(); //get actions that are on agent
        foreach (GAction a in acts)
            actions.Add(a);
    }

    private bool invoked = false;

    private void CompleteAction() //when we finish a current action
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //if we are in the middle of performing an action, exit LateUpdate()
        if (currentAction != null && currentAction.running)
        {
            if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < 1f)
            {
                //we've reached our goal location, so we need to wait for the task duration time
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return; //exit because the agent has plans
        }

        //get new action
        if (planner == null && actionQueue == null) //agent has no plans to work on
        {
            planner = new GPlanner();

            //put goals in order from most important to least important
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.Plan(actions, sg.Key.sgoals, beliefs); //null if only using world goals
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break; //no need to keep looking for a plan if we have one
                }
            }
        }

        //
        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        //condition to consider
        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                if (currentAction.target != null) //set destination for our agent
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;//force us to get a new plan, make sure agent doesn't get stuck in the middle of a plan
            }
        }
    }
}