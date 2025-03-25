namespace RPG_wiedzmin_wanna_be.Game
{
    internal class InstructionBuilder
    {
        private List<string> instructions = new List<string>();

        public InstructionBuilder AddMovement()
        {
            instructions.Add("W,A,S,D - movement");
            return this;
        }

        public InstructionBuilder AddPickupItemInstruction(bool map_has_items, bool player_has_items)
        {
            string ins = "E - ";
            if (map_has_items)
            {
                ins += "pickup item";
            }
            if (map_has_items && player_has_items)
            {
                ins += "/";
            }
            if (player_has_items)
            {
                ins += "drop item";
            }
            instructions.Add(ins);
            return this;
        }

        public InstructionBuilder AddEquipInstruction(bool has_equipable, bool has_potions)
        {
            string ins = "F -";
            if (has_equipable)
            {
                ins += " equip";
            }
            if(has_potions && has_equipable)
            {
                ins += "/";
            }
            if (has_potions)
            {
                ins += "drink potion";
            }
            instructions.Add(ins);
            return this;
        }

        public InstructionBuilder AddInventoryInstruction()
        {

            instructions.Add("Q - inventory");
            return this;
        }

        public InstructionBuilder AddSwapHandsInstruction()
        {
            instructions.Add("R - swap hands");
            return this;
        }

        public List<string> Build()
        {
            return instructions;
        }

    }
}
