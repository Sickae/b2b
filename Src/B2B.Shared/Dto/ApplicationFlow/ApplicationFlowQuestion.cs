namespace B2B.Shared.Dto.ApplicationFlow
{
    public class ApplicationFlowQuestion
    {
        public string Code { get; set; }
        public ApplicationFlowQuestionType Type { get; set; }
        public string Text { get; set; }
        public ApplicationFlowQuestionChoice[] Choices { get; set; }
        public bool IsInlineChoice { get; set; }
        public bool IsOptional { get; set; }
    }
}
