using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.State
{
    public class SetupStateStore
    {
        public SetupState CurrentState { get; private set; }

        public SetupStateStore()
        {
            CurrentState = new SetupState();
        }

        public void UpdateName(string name)
        {
            CurrentState.NewUser.Name = name;
        }

        public void AddCompetence(Competence competence, string type)
        {
            if (type == "Interest")
                CurrentState.AddInterest(competence);
            else if (type == "Skill")
                CurrentState.AddSkill(competence);
        }

        public void SetProfilePicture(byte[] blob)
        {
            CurrentState.NewUser.ProfilePicture = blob;
        }

        public void SetLearningTools(List<LearningTool> tools)
        {
            CurrentState.SetLearningTools(tools);
        }

        public void SetGoalCompetence(Competence competence)
        {
            CurrentState.SetChosenCompetence(competence);
        }

        public void SetTargetDate(System.DateTime date)
        {
            CurrentState.SetTargetDate(date);
        }

        public void Clear()
        {
            CurrentState.ClearAll();
        }
    }
}