namespace SimPressBusinessLogic.Entities.PresentationEntities
{
    public class SlideInfo
    {
        public int SlideNumber { get; set; }
        public SlideTextParameters HeaderText { get; set; }
        public SlideTextParameters ContentText { get; set; }
        public string FonColor { get; set; }
        public SlideImageParameters Image { get; set; }
    }
}
