namespace PersonalProject.Models
{
    public class Instruction
    {
        public int InstructionID { get; set; }
        public string InstructionDescription { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}
