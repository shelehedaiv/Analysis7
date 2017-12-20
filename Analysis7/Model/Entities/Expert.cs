﻿using System.Collections.Generic;
using System.Linq;
using Analysis7.Model.Observer;

namespace Analysis7.Model.Entities
{
    public class Expert: Subject,IListener
    {
        private readonly List<Event> _bindedEvents;
        public int Number { get; set; }
        public Probability AverageProbability { get; set; }
        private readonly ExpertCoefficient _coefficient;
        public int Coefficient
        {
            get => _coefficient.Value;
            set
            {
                _coefficient.Value = value;
                foreach (var ev in _bindedEvents)
                {
                    ev.UpdateCoefficient(Number,_coefficient.Value);
                }
            }
        }

        public Expert(int number, List<Event> bindedEvents)
        {
            Number = number;
            _coefficient = new ExpertCoefficient(1);
            _bindedEvents = bindedEvents;
            foreach (var ev in _bindedEvents)
            {
                ev.AttachListener(this);
            }
        }

        public void Update()
        {
            AverageProbability = new Probability(_bindedEvents.Average(ev => ev.ExpertProbabilities[Number].Value));
        }
    }
}





