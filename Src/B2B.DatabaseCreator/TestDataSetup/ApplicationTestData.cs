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
                        Code = "first_question",
                        Type = ApplicationFlowQuestionType.Text,
                        Text = "First question"
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "second_question",
                        Type = ApplicationFlowQuestionType.Choice,
                        Text = "Second question",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "Option 1", Ordinal = 0},
                            new() {Value = "Option 2", Ordinal = 1},
                            new() {Value = "Option 3", Ordinal = 2}
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "third_question",
                        Type = ApplicationFlowQuestionType.MultiChoice,
                        Text = "Third question",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "Option 1", Ordinal = 0},
                            new() {Value = "Option 2", Ordinal = 1},
                            new() {Value = "Option 3", Ordinal = 2}
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "last_optional_question",
                        Type = ApplicationFlowQuestionType.Text,
                        Text = "Optional question",
                        IsOptional = true
                    }
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
