using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //because we will use a navmeshagent

[RequireComponent(typeof(NavMeshAgent))]
public abstract class GAction : MonoBehaviour //abstract because we are going to inherit from it
{
    public string actionName = "Action"; //default action name
    public float cost = 1.0f; //planner will find the cheapest plan
    public GameObject target; //target where action takes place
    public GameObject targetTag; //find game objects with tag in case no target set
    public float duration = 0; //how long will agent spend at target performing action
    public WorldState[] preConditions; //world states needed for actions, gotten from inspector
    public WorldState[] afterEffects; //
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions; //ease of use for world conditions
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs; //internal/local states

    public bool running = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        //setup preconditions and effects dictionaries
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key)) { return false; } //if one required condition is missing, return false
        }
        return true;//if all conditions found we can return true
    }

    public abstract bool PrePerform();

    public abstract bool PostPerform();
}