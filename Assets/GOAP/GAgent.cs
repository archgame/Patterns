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

    private GPlanner planner; //class that does the planning
    private Queue<GAction> actionQueue;
    public GAction currentAction;
    private SubGoal currentGoal;

    // Start is called before the first frame update
    private void Start()
    {
        GAction[] acts = this.GetComponents<GAction>(); //get actions that are on agent
        foreach (GAction a in acts)
            actions.Add(a);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
    }
}