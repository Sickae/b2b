using System;
using B2B.DataAccess.Entities;
using B2B.Shared.Dto.ApplicationFlow;
using Newtonsoft.Json;

namespace B2B.DatabaseCreator.TestDataSetup
{
    public partial class TestData
    {
        protected virtual void CreateApplicationFlows()
        {
            ApplicationFlows = new[]
            {
                CreateStaticApplicationFlow()
            };
        }

        private ApplicationFlowEntity CreateStaticApplicationFlow()
        {
            var description = new ApplicationFlowDescription
            {
                Questions = new[]
                {
                    new ApplicationFlowQuestion
                    {
                        Type = ApplicationFlowQuestionType.Text,
                        Text = "First question"
                    },
                    new ApplicationFlowQuestion
                    {
                        Type = ApplicationFlowQuestionType.Choice,
                        Text = "Second question"
                    },
                    new ApplicationFlowQuestion
                    {
                        Type = ApplicationFlowQuestionType.MultiChoice,
                        Text = "Third question"
                    },
                }
            };

            return new ApplicationFlowEntity
            {
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow,
                DescriptionJson = JsonConvert.SerializeObject(description)
            };
        }
    }
}
