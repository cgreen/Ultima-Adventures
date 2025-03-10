using System;
using Server.Engines.Craft;
using Server.Items;

namespace Server.Items
{
	public class PlateOfHonorGorget : BaseArmor, IBlacksmithRepairable
    {
		public override int LabelNumber{ get{ return 1074303; } }
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 45; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PlateOfHonorGorget() : base( 11022 )
		{
			Weight = 2.0;
			Attributes.RegenHits = 1;
			Attributes.AttackChance = 5;
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			
			list.Add( 1072376, "6" );
			
			if ( this.Parent is Mobile )
			{
				if ( this.Hue == 0x47E )
				{
					list.Add( 1072377 );
					list.Add( 1072513, "25" );
					list.Add( "Chivalry 10 (total)" );
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( this.Hue == 0x0 )
			{
				list.Add( 1072378 );
				list.Add( 1072382, "2" );
				list.Add( 1072383, "5" );
				list.Add( 1072384, "5" );
				list.Add( 1072385, "3" );
				list.Add( 1072386, "5" );
				list.Add( 1060450, "3" );
				list.Add( 1072513, "25" );
				list.Add( "Chivalry 10 (total)" );
				list.Add( 1060441 );
			}
		}

		public override bool OnEquip( Mobile from )
		{
			
			Item shirt = from.FindItemOnLayer( Layer.InnerTorso );
			Item glove = from.FindItemOnLayer( Layer.Gloves );
			Item pants = from.FindItemOnLayer( Layer.Pants );
			Item helm = from.FindItemOnLayer( Layer.Helm );
			Item arms = from.FindItemOnLayer( Layer.Arms );
			
			if ( shirt != null && shirt.GetType() == typeof( PlateOfHonorChest ) && glove != null && glove.GetType() == typeof( PlateOfHonorGloves ) && pants != null && pants.GetType() == typeof( PlateOfHonorLegs ) && helm != null && helm.GetType() == typeof( PlateOfHonorHelm ) && arms != null && arms.GetType() == typeof( PlateOfHonorArms ) )
			{
				Effects.PlaySound( from.Location, from.Map, 503 );
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );

				Hue = 0x47E;
				ArmorAttributes.SelfRepair = 3;
				Attributes.NightSight = 1;
				PhysicalBonus = 2;
				FireBonus = 5;
				ColdBonus = 5;
				PoisonBonus = 3;
				EnergyBonus = 5;

				PlateOfHonorChest chest = from.FindItemOnLayer( Layer.InnerTorso ) as PlateOfHonorChest;
				PlateOfHonorGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as PlateOfHonorGloves;
				PlateOfHonorLegs legs = from.FindItemOnLayer( Layer.Pants ) as PlateOfHonorLegs;
				PlateOfHonorHelm helmet = from.FindItemOnLayer( Layer.Helm ) as PlateOfHonorHelm;
				PlateOfHonorArms arm = from.FindItemOnLayer( Layer.Arms ) as PlateOfHonorArms;

				chest.Hue = 0x47E;
				chest.Attributes.NightSight = 1;
				chest.Attributes.ReflectPhysical = 25;
				chest.ArmorAttributes.SelfRepair = 3;
				chest.SkillBonuses.SetValues( 0, SkillName.Chivalry, 10.0 );
				chest.PhysicalBonus = 2;
				chest.FireBonus = 5;
				chest.ColdBonus = 5;
				chest.PoisonBonus = 3;
				chest.EnergyBonus = 5;
				
				gloves.Hue = 0x47E;
				gloves.Attributes.NightSight = 1;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.PhysicalBonus = 2;
				gloves.FireBonus = 5;
				gloves.ColdBonus = 5;
				gloves.PoisonBonus = 3;
				gloves.EnergyBonus = 5;

				legs.Hue = 0x47E;
				legs.Attributes.NightSight = 1;
				legs.ArmorAttributes.SelfRepair = 3;
				legs.PhysicalBonus = 2;
				legs.FireBonus = 5;
				legs.ColdBonus = 5;
				legs.PoisonBonus = 3;
				legs.EnergyBonus = 5;

				helmet.Hue = 0x47E;
				helmet.Attributes.NightSight = 1;
				helmet.ArmorAttributes.SelfRepair = 3;
				helmet.PhysicalBonus = 2;
				helmet.FireBonus = 5;
				helmet.ColdBonus = 5;
				helmet.PoisonBonus = 3;
				helmet.EnergyBonus = 5;
				
				arm.Hue = 0x47E;
				arm.Attributes.NightSight = 1;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.PhysicalBonus = 2;
				arm.FireBonus = 5;
				arm.ColdBonus = 5;
				arm.PoisonBonus = 3;
				arm.EnergyBonus = 5;
										
				from.SendLocalizedMessage( 1072391 );
			}
			this.InvalidateProperties();
			return base.OnEquip( from );							
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = ( Mobile )parent;
				Hue = 0x0;
				ArmorAttributes.SelfRepair = 0;
				Attributes.NightSight = 0;
				PhysicalBonus = 0;
				FireBonus = 0;
				ColdBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;
				if ( m.FindItemOnLayer( Layer.InnerTorso ) is PlateOfHonorChest && m.FindItemOnLayer( Layer.Gloves ) is PlateOfHonorGloves && m.FindItemOnLayer( Layer.Arms ) is PlateOfHonorArms && m.FindItemOnLayer( Layer.Pants ) is PlateOfHonorLegs && m.FindItemOnLayer( Layer.Helm ) is PlateOfHonorHelm )
				{
					PlateOfHonorChest chest = m.FindItemOnLayer( Layer.InnerTorso ) as PlateOfHonorChest;
					chest.Hue = 0x0;
					chest.Attributes.NightSight = 0;
					chest.Attributes.ReflectPhysical = 0;
					chest.ArmorAttributes.SelfRepair = 0;
					chest.SkillBonuses.SetValues( 0, SkillName.Chivalry, 0.0 );
					chest.PhysicalBonus = 0;
					chest.FireBonus = 0;
					chest.ColdBonus = 0;
					chest.PoisonBonus = 0;
					chest.EnergyBonus = 0;

					PlateOfHonorGloves gloves = m.FindItemOnLayer( Layer.Gloves ) as PlateOfHonorGloves;
					gloves.Hue = 0x0;
					gloves.Attributes.NightSight = 0;
					gloves.ArmorAttributes.SelfRepair = 0;
					gloves.PhysicalBonus = 0;
					gloves.FireBonus = 0;
					gloves.ColdBonus = 0;
					gloves.PoisonBonus = 0;
					gloves.EnergyBonus = 0;
					
					PlateOfHonorArms arm = m.FindItemOnLayer( Layer.Arms ) as PlateOfHonorArms;
					arm.Hue = 0x0;
					arm.Attributes.NightSight = 0;
					arm.ArmorAttributes.SelfRepair = 0;
					arm.PhysicalBonus = 0;
					arm.FireBonus = 0;
					arm.ColdBonus = 0;
					arm.PoisonBonus = 0;
					arm.EnergyBonus = 0;

					PlateOfHonorLegs legs = m.FindItemOnLayer( Layer.Pants ) as PlateOfHonorLegs;
					legs.Hue = 0x0;
					legs.Attributes.NightSight = 0;
					legs.ArmorAttributes.SelfRepair = 0;
					legs.PhysicalBonus = 0;
					legs.FireBonus = 0;
					legs.ColdBonus = 0;
					legs.PoisonBonus = 0;
					legs.EnergyBonus = 0;

					PlateOfHonorHelm helmet = m.FindItemOnLayer( Layer.Helm ) as PlateOfHonorHelm;
					helmet.Hue = 0x0;
					helmet.Attributes.NightSight = 0;
					helmet.ArmorAttributes.SelfRepair = 0;
					helmet.PhysicalBonus = 0;
					helmet.FireBonus = 0;
					helmet.ColdBonus = 0;
					helmet.PoisonBonus = 0;
					helmet.EnergyBonus = 0;
				}
				this.InvalidateProperties();
			}
			base.OnRemoved( parent );
		}

		public PlateOfHonorGorget( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}