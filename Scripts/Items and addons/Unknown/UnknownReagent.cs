using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using System.Globalization;

namespace Server.Items
{
	public class UnknownReagent : Item
	{
		public int RegAmount;

		[CommandProperty(AccessLevel.Owner)]
		public int Reg_Amount { get { return RegAmount; } set { RegAmount = value; InvalidateProperties(); } }

		[Constructable]
		public UnknownReagent() : base( 0x0EFC )
		{
			RegAmount = Utility.RandomMinMax( 1, 10 );
			string sContainer = "jar of reagents";

			switch( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: ItemID = 0x1005; sContainer = "jar of reagents"; break;
				case 1: ItemID = 0x1006; sContainer = "jar of reagents"; break;
				case 2: ItemID = 0x1007; sContainer = "jar of reagents"; break;
				case 3: ItemID = 0x9C8; sContainer = "jug of reagents"; break;
			}

			string sLiquid = "a strange";
			switch( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: sLiquid = "an odd"; break;
				case 1: sLiquid = "an unusual"; break;
				case 2: sLiquid = "a bizarre"; break;
				case 3: sLiquid = "a curious"; break;
				case 4: sLiquid = "a peculiar"; break;
				case 5: sLiquid = "a strange"; break;
				case 6: sLiquid = "a weird"; break;
			}
			Name = sLiquid + " " + sContainer;
			Hue = RandomThings.GetRandomColor(0);
			Weight = 1.0;
			Amount = 1;
			Stackable = false;
		}

		public UnknownReagent( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
			{
				from.SendMessage( "That cannot move so you cannot identify it." );
				return;
			}
			else if ( !from.InRange( this.GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will need to get closer to identify that." );
				return;
			}
			else if ( !IsChildOf( from.Backpack ) && Server.Misc.MyServerSettings.IdentifyItemsOnlyInPack() ) 
			{
				from.SendMessage( "This must be in your backpack to identify." );
				return;
			}
			else
			{
				if ( from.CheckSkill( SkillName.TasteID, -5, 125 ) )
				{
					int QtyBonus = 0;
					if ( from.Skills[SkillName.Cooking].Value >= 25.0 )
					{
						QtyBonus = (int)( from.Skills[SkillName.Cooking].Value / 5 );
					}

					from.PlaySound( Utility.Random( 0x3A, 3 ) );

					if ( from.Body.IsHuman && !from.Mounted )
						from.Animate( 34, 5, 1, true, false, 0 );

					int RegCount = this.RegAmount + QtyBonus;
					if ( RegCount < 1 ){ RegCount = 1; }

					Server.Items.UnknownReagent.GiveReagent( from, RegCount, this );
				}
				else
				{
					int nReaction = Utility.RandomMinMax( 0, 10 );

					if ( nReaction < 3 )
					{
						from.PlaySound( from.Female ? 813 : 1087 );
						from.Say( "*vomits*" );
						if ( !from.Mounted ) 
							from.Animate( 32, 5, 1, true, false, 0 );                     
						Vomit puke = new Vomit(); 
						puke.Map = from.Map; 
						puke.Location = from.Location;
						from.SendMessage("Making you sick to your stomach, you toss it out.");
					}
					else if ( nReaction > 6 )
					{
						int nPoison = Utility.RandomMinMax( 0, 10 );
						from.Say( "Poison!" );
						from.PlaySound( Utility.Random( 0x3A, 3 ) );
							if ( nPoison > 9 ) { from.ApplyPoison( from, Poison.Deadly ); }
							else if ( nPoison > 7 ) { from.ApplyPoison( from, Poison.Greater ); }
							else if ( nPoison > 4 ) { from.ApplyPoison( from, Poison.Regular ); }
							else { from.ApplyPoison( from, Poison.Lesser ); }
						from.SendMessage( "Poison!");
					}
					else
					{
						from.PlaySound( Utility.Random( 0x3A, 3 ) );
						if ( from.Body.IsHuman && !from.Mounted )
							from.Animate( 34, 5, 1, true, false, 0 );
						from.SendMessage("Failing to identify the reagent, you toss it out.");
					}
				}

				this.Delete();
			}
		}

		public static void MakeSpaceAceReagent( Item item )
		{
			item.ItemID = 0x1FDC;
			item.Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string sLiquid = "a strange";
			switch( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: sLiquid = "an odd"; break;
				case 1: sLiquid = "an unusual"; break;
				case 2: sLiquid = "a bizarre"; break;
				case 3: sLiquid = "a curious"; break;
				case 4: sLiquid = "a peculiar"; break;
				case 5: sLiquid = "a strange"; break;
				case 6: sLiquid = "a weird"; break;
			}
			item.Name = sLiquid + " flask of reagents";
		}

		public static void GiveReagent( Mobile from, int qty, Item usedItem )
		{
			string regs = "";

			Item ingredient = new BlackPearl(); ingredient.Delete();

			int mainList = Utility.RandomList( 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 );
			int necroList = Utility.RandomList( 8, 9, 10, 11, 12 );
			int mixList = Utility.RandomList( 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 );
			int herbList = Utility.RandomList( 25, 26, 27, 28, 29, 30 );

			if ( Server.Misc.IntelligentAction.TestForReagent( from, "herbalist" ) && Utility.RandomMinMax( 1, 2 ) == 1 )
				mainList = herbList;

			if ( Server.Misc.IntelligentAction.TestForReagent( from, "mixologist" ) && Utility.RandomMinMax( 1, 2 ) == 1 )
				mainList = mixList;

			if ( Server.Misc.IntelligentAction.TestForReagent( from, "necromancer" ) && Utility.RandomMinMax( 1, 4 ) > 1 )
				mainList = necroList;

			switch ( mainList )
			{
				case 0: ingredient = new BlackPearl( qty ); break; 
				case 1: ingredient = new Bloodmoss( qty ); break; 
				case 2: ingredient = new Garlic( qty ); break; 
				case 3: ingredient = new Ginseng( qty ); break; 
				case 4: ingredient = new MandrakeRoot( qty ); break; 
				case 5: ingredient = new Nightshade( qty ); break; 
				case 6: ingredient = new SpidersSilk( qty ); break; 
				case 7: ingredient = new SulfurousAsh( qty ); break; 

				case 8: ingredient = new BatWing( qty ); break; 
				case 9: ingredient = new GraveDust( qty ); break; 
				case 10: ingredient = new DaemonBlood( qty ); break; 
				case 11: ingredient = new PigIron( qty ); break; 
				case 12: ingredient = new NoxCrystal( qty ); break; 

				case 13: ingredient = new EyeOfToad( qty ); break; 
				case 14: ingredient = new FairyEgg( qty ); break; 
				case 15: ingredient = new GargoyleEar( qty ); break; 
				case 16: ingredient = new BeetleShell( qty ); break; 
				case 17: ingredient = new MoonCrystal( qty ); break; 
				case 18: ingredient = new PixieSkull( qty ); break; 
				case 19: ingredient = new RedLotus( qty ); break; 
				case 20: ingredient = new SeaSalt( qty ); break; 
				case 21: ingredient = new SilverWidow( qty ); break; 
				case 22: ingredient = new SwampBerries( qty ); break; 
				case 23: ingredient = new Brimstone( qty ); break; 
				case 24: ingredient = new ButterflyWings( qty ); break; 

				case 25: ingredient = new PlantHerbalism_Leaf(); ingredient.Amount = qty; break; 
				case 26: ingredient = new PlantHerbalism_Flower(); ingredient.Amount = qty; break; 
				case 27: ingredient = new PlantHerbalism_Mushroom(); ingredient.Amount = qty; break; 
				case 28: ingredient = new PlantHerbalism_Lilly(); ingredient.Amount = qty; break; 
				case 29: ingredient = new PlantHerbalism_Cactus(); ingredient.Amount = qty; break; 
				case 30: ingredient = new PlantHerbalism_Grass(); ingredient.Amount = qty; break; 
			}

			ItemIdentification.ReplaceItemOrAddToBackpack(usedItem, ingredient, from);
			
			regs = ingredient.Name;

			if ( regs == null )
			{
				regs = Server.Misc.MorphingItem.AddSpacesToSentence( (ingredient.GetType()).Name );
				regs = regs.ToLower(new CultureInfo("en-US", false));
			}

			if ( qty < 2 ){ from.SendMessage("This seems to be " + regs + "."); }
			else { from.SendMessage("This seems to be " + qty + " " + regs + "."); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, RegAmount + " of this Unknown Reagent in Here");
			list.Add( 1049644, "Unidentified"); // PARENTHESIS
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( RegAmount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RegAmount = reader.ReadInt();
				if ( RegAmount < 1 ){ RegAmount = Utility.RandomMinMax( 1, 10 ); }
		}
	}
}