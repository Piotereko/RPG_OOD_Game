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

        public InstructionBuilder AddPickupItemInstruction()
        {

            instructions.Add("E - Pick up item");
            return this;
        }

        public InstructionBuilder AddDropInstruction(bool player_in_inventor)
        {
            instructions.Add("Q - Drop item");
            if (player_in_inventor)
            {
                instructions.Add("Z - Drop all items");
            }
            return this;
        }
        public InstructionBuilder AddInventoryMoveing(bool player_has_items, bool player_in_inventory)
        {
            if (player_in_inventory)
            {
                if (player_has_items)
                {
                    instructions.Add("1 - Previous item");
                    instructions.Add("2 - Next item");
                }
            }
            else
            {
                instructions.Add("1 - Left hand");
                instructions.Add("2 - Right hand");
            }
            return this;
        }
        public InstructionBuilder AddModeSwitch()
        {

            instructions.Add("R - Switch inventory/hands");
            return this;
        }
        public InstructionBuilder AddEquipInstruction(bool has_equipable, bool has_potions, bool player_in_inventory, bool has_item_hand)
        {

            if (player_in_inventory)
            {
                string ins = "F -";
                if (has_equipable)
                {
                    ins += " equip";
                }
                if (has_potions && has_equipable)
                {
                    ins += "/";
                }
                if (has_potions)
                {
                    ins += "drink potion";
                }
                instructions.Add(ins);
            }
            else
            {
                if (has_item_hand)
                    instructions.Add("F - Unequip item");
            }

            return this;
        }

        public InstructionBuilder AddExitInstruction()
        {
            instructions.Add("ESC - Exit game");
            return this;
        }

        public List<string> Build()
        {
            return instructions;
        }

    }
}
