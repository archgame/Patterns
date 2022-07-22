using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //used for sorting dictionary

public class Node //used for creating tree
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);//make a copy of the dictionary
        this.action = action;
    }

    public Node(Node parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);//make a copy of the dictionary
        foreach (KeyValuePair<string, int> b in beliefstates)
        {
            if (!this.state.ContainsKey(b.Key))
            {
                this.state.Add(b.Key, b.Value);
            }
        }
        this.action = action;
    }
}

public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefstates)
    {
        //find out which actions are useable
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
                usableActions.Add(a);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), beliefstates.GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        //guard statement if action tree was found
        if (!success) { Debug.Log("No Plan Found"); return null; }

        //if we get this far, we have a plan
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        //now that we have cheapest leaf, we work backwards up it to find the chain of actions
        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null) //once n is equal to null, it means we've reached the start node
        {
            if (n.action != null)
            {
                result.Insert(0, n.action); //always add action to the beginning of the list
            }
            n = n.parent; //a type of recursion
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result)
        {
            queue.Enqueue(a); //transitioning from list to the queue
        }

        //we have a plan, let's debug what the plan is
        Debug.Log("The Plan is: ");
        foreach (GAction a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    //use recursion to build action graph (when a method calls itself)
    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> useableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false; //start by assuming we have not found an action
        foreach (GAction action in useableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state); //first time through, filled with states of the world, keep track of changes, match as we go along
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value); //add the effects of the current action
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);
                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(useableActions, action); //remove action we just used (we can't create a circular path
                    bool found = BuildGraph(node, leaves, subset, goal); //recursion
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key)) { return false; }
        }
        return true;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }
}