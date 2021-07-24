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
                        Text = "Second question",
                        Choices = new ApplicationFlowQuestionChoice []
                        {
                            new()
                            {
                                Code = "second_question",
                                Value = "Option 1"
                            },
                            new()
                            {
                                Code = "second_question",
                                Value = "Option 2"
                            },
                            new()
                            {
                                Code = "second_question",
                                Value = "Option 3"
                            }
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Type = ApplicationFlowQuestionType.MultiChoice,
                        Text = "Third question",
                        Choices = new ApplicationFlowQuestionChoice []
                        {
                            new()
                            {
                                Code = "third_question",
                                Value = "Option 1"
                            },
                            new()
                            {
                                Code = "third_question",
                                Value = "Option 2"
                            },
                            new()
                            {
                                Code = "third_question",
                                Value = "Option 3"
                            }
                        }
                    },
                }
            };

            return new ApplicationFlowEntity
            {
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow,
                Description = "Please answer the questions below to submit an application to the guild.",
                DescriptionJson = JsonConvert.SerializeObject(description)
            };
        }
    }
}
