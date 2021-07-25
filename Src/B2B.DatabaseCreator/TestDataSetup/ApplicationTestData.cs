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
                        Code = "class",
                        Type = ApplicationFlowQuestionType.Choice,
                        Text = "What class are you playing as your main?",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "Blader", Ordinal = 0},
                            new() {Value = "Wizard", Ordinal = 1},
                            new() {Value = "Warrior", Ordinal = 2},
                            new() {Value = "Gladiator", Ordinal = 3},
                            new() {Value = "Force Shielder", Ordinal = 4},
                            new() {Value = "Force Archer", Ordinal = 5},
                            new() {Value = "Force Gunner", Ordinal = 6},
                            new() {Value = "Force Blader", Ordinal = 7},
                            new() {Value = "Dark Mage", Ordinal = 8},
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "play_time",
                        Type = ApplicationFlowQuestionType.Choice,
                        Text = "How long have you been playing Cabal Online?",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "Less than 1 year", Ordinal = 0},
                            new() {Value = "1-5 years", Ordinal = 1},
                            new() {Value = "5-10 years", Ordinal = 2},
                            new() {Value = "More than 10 years", Ordinal = 3}
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "source",
                        Type = ApplicationFlowQuestionType.MultiChoice,
                        Text = "Why would you like to join us?",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "For the dungeons", Ordinal = 0},
                            new() {Value = "For the community", Ordinal = 1},
                            new() {Value = "Can't find any other guild", Ordinal = 2},
                            new() {Value = "To scam", Ordinal = 3},
                        }
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "likely_to_quit",
                        Type = ApplicationFlowQuestionType.Choice,
                        Text = "How likely are you to quit or take a break from the game in the next 3 months?",
                        Choices = new ApplicationFlowQuestionChoice[]
                        {
                            new() {Value = "Definitely not", Ordinal = 0},
                            new() {Value = "Not likely", Ordinal = 1},
                            new() {Value = "Likely", Ordinal = 2},
                            new() {Value = "Guaranteed", Ordinal = 3},
                        },
                        IsInlineChoice = true,
                    },
                    new ApplicationFlowQuestion
                    {
                        Code = "about_yourself",
                        Type = ApplicationFlowQuestionType.Text,
                        Text = "Write something about yourself.",
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
