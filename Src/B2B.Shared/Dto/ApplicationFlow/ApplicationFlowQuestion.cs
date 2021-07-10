namespace B2B.Shared.Dto.ApplicationFlow
{
    public class ApplicationFlowQuestion
    {
        public ApplicationFlowQuestionType Type { get; set; }
        public string Text { get; set; }
        public ApplicationFlowQuestionChoice[] Choices { get; set; }
    }
}
