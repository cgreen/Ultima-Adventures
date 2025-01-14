using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
using System.Net;
using Server.Accounting;
using Server.Mobiles;
using Server.Commands;
using Server.Regions;
using Server.Spells;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	[Flipable(0x1E5E, 0x1E5F)]
	public class SearchBoard : Item
	{
		[Constructable]
		public SearchBoard( ) : base( 0x1E5E )
		{
			Weight = 1.0;
			Name = "Sage Advice";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "The Search for Artifacts" );
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( SearchBoardGump ) );
				e.SendGump( new SearchBoardGump( e ) );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public class SearchBoardGump : Gump
		{
			public SearchBoardGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 151);
				AddImage(300, 0, 151);
				AddImage(0, 300, 151);
				AddImage(300, 300, 151);
				AddImage(600, 0, 151);
				AddImage(600, 300, 151);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(301, 298, 129);
				AddImage(598, 298, 129);
				AddImage(8, 11, 145);
				AddImage(267, 19, 141);
				AddImage(473, 19, 141);
				AddImage(698, 7, 146);
				AddImage(219, 14, 143);
				AddImage(249, 31, 159);
				AddImage(50, 260, 161);
				AddImage(853, 210, 161);
				AddImage(853, 257, 161);
				AddImage(854, 554, 157);
				AddImage(51, 554, 157);
				AddItem(179, 49, 7775);
				AddHtml( 234, 72, 428, 27, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SAGE ADVICE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 100, 155, 737, 418, @"<BODY><BASEFONT Color=#FCFF00><BIG>If you have a grand quest in search of a precious artifact, you can seek the advice of sages in your journey. Their advice is not cheap, where you may be spending 10,000 gold for the best advice they can give. To begin your quest, visit one of the many sages in the land and give them enough gold for their advice. They will give you an artifact encyclopedia from which you can search for the first clues on the whereabouts of your artifact. These encyclopedias come in varying degrees of accuracy, depending on how much gold you are willing to part with.<br><br>Legend Lore<br><br>  Level 1 - 10,000 Gold<br>  Level 2 - 12,000 Gold<br>  Level 3 - 14,000 Gold<br>  Level 4 - 16,000 Gold<br>  Level 5 - 18,000 Gold<br>  Level 6 - 20,000 Gold<br><br>Sages are never able to give you absolute accurate information on the location of an artifact, but the higher the encyclopedia's lore level, the better the chances you may find it. Once you receive your encyclopedia, open it up and choose an artifact from its many pages. After selecting an artifact, you will tear out the appropriate page and toss out the rest of the book. This page will give you your first clue on where to search. Areas the artifact may be in could span many different lands or worlds, where some you may have never been or heard of. You will be provided with the coordinates of the place you seek, so make sure you have a sextant with you.<br><br>Throughout history, many people kept these artifacts stored on blocks of crafted stone. These crafted stones are often decorated with a symbol on the surface, where a metal chest rests and the item may be inside. Some treasure hunters find the chests empty, realizing the legends were false. The better the lore level of the book you ripped the page from, the better the chance you will find the artifact. If nothing else, you may find a large sum of gold to cover some of your expenses on this journey. Some may provide a new clue on where the artifact is, and you will update your notes when these clues are found. The most disappointing search may yield a fake artifact. These turn out to be useless items that simply look like the artifact you were searching for. <br><br>These quests are quite involved and you may only participate in one such quest at a time. If you have not finished a quest, and try to seek a sage for another, you will find that the page of your prior quest will have gone missing. It would have been surely lost somewhere. If you finish a quest, either with success or failure, a sage will not have any new advice for you for at least two weeks so you will have to wait to begin a new quest. So good luck treasure hunter, and may the gods aid you in your journey.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public SearchBoard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}