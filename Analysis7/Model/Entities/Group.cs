﻿using System.Collections.Generic;
using System.Linq;
using Analysis7.Model.Observer;

namespace Analysis7.Model.Entities
{
    public class Group:RiskEntity, IListener, IAverageProbability
    {
        public List<Event> RiskEvents { get; set; }
        public List<Expert> ProbabilityExperts { get; set; }
        public List<Expert> PriceExperts { get; set; }
        public List<Source> RiskSources { get; set; }
        public Probability AverageProbability {get; set;}

        public Group(string groupName,string description, List<Event> currentGroupRiskEvents, List<Source> currentGroupRiskSources) :base(groupName, description)
        {
            RiskEvents = currentGroupRiskEvents;
            RiskSources = currentGroupRiskSources;
            foreach (var riskEvent in RiskEvents)
            {
                riskEvent.AttachListener(this);
            }
            ProbabilityExperts=new List<Expert>();//todo return
            for (int i = 0; i < 10; i++)
            {
                ProbabilityExperts.Add(new Expert(i,  RiskEvents.Select(e => e.Probability).ToList()));
            }
            PriceExperts = new List<Expert>();
            for (int i = 0; i < 10; i++)
            {
                PriceExperts.Add(new Expert(i, RiskEvents.Select(e=> (ProbabilityEntity)e.Price).ToList()));
            }
            foreach (var expert in ProbabilityExperts  )     {
                expert.AttachListener(this);
            }
            foreach (var riskSource in RiskSources)
            {
                riskSource.AttachListener(this);
            }
            Update();
        }
        
        /// !!!!!!!
        
        public void Update()
        {
<<<<<<< HEAD
            AverageProbability = new Probability(RiskEvents.Average(e => e.Probability.AverageProbability.Value));
=======
            if (RiskEvents.Any(e=>e.Status))
                AverageProbability = new Probability(RiskEvents.Where(e=>e.Status).Average(e => e.AverageProbability.Value));
            else AverageProbability=new Probability(0);
>>>>>>> c3f1630288749a0d785139996f6f7bc855404f48
            Notify();
        }
    }
}