using System;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;
using XCom;
using System.Windows.Forms;

namespace XCom
{
	public delegate void BufferChangedDelegate(Node current);

	public class xConsole
	{
		private static Node currLine=null;
		private static int numNodes;

		public static event BufferChangedDelegate BufferChanged;

		private xConsole()
		{
		}

		public static Node CurrNode
		{
			get{return currLine;}
		}

		public static int NumNodes
		{
			get{return numNodes;}
		}

		private static void makeNodes(int numLines)
		{
			if(currLine == null)
			{
				currLine = new Node("");
				currLine.next = new Node("");

				Node curr = currLine.next;
				Node last=currLine;
				curr.last=last;
				for(int i=2;i<numLines;i++)
				{
					curr.next=new Node("");
					curr=curr.next;
					curr.last = last.next;
					last = last.next;
				}

				curr.next=currLine;
				currLine.last = curr;
			}
			else
			{
				if(numLines>numNodes)
				{
					Node curr = currLine;
					Node last = currLine.last;

					for(int i=0;i<numLines-numNodes;i++)
					{
						Node n = new Node("");
						n.next=curr;
						n.last=last;
						last.next=n;
						curr.last=n;
						last=n;
					}
				}
				else
				{
					for(int i=0;i<numNodes-numLines;i++)
					{
						currLine.last = currLine.last.last;
						currLine.last.next = currLine;
					}
				}
			}
		}

//		public static xConsole Instance
//		{
//			get{if(console==null)console = new xConsole(20);return console;}
//		}

		public static void Init(int numLines)
		{			
			makeNodes(numLines);
			numNodes = numLines;
		}

		public static void AddLine(string s)
		{
			currLine = currLine.last;
			currLine.str=s;

			if(BufferChanged!=null)
				BufferChanged(currLine);
		}

		public static void SetLine(string s)
		{
			currLine.str=s;
			if(BufferChanged!=null)
				BufferChanged(currLine);
		}

		public static void AddToLine(string s)
		{
			currLine.str+=s;
			if(BufferChanged!=null)
				BufferChanged(currLine);
		}
		//
		//		public void KeyDown(object sender, KeyEventArgs e)
		//		{
		//			switch(e.KeyCode)
		//			{
		//				case Keys.Enter:
		//					AddLine(command);
		//					if(ExecuteCommand != null)
		//						ExecuteCommand(command);
		//					command="";
		//					break;
		//				case Keys.ShiftKey:
		//					shift=true;
		//					break;
		//				case Keys.Back:
		//					if(command.Length>0)
		//						command = command.Substring(0,command.Length-1);
		//					break;
		//				case Keys.Oemtilde:
		//					if(ExecuteCommand != null)
		//						ExecuteCommand("flipLast");
		//					break;
		//				default:
		//					char ch = (((char)e.KeyValue)+"").ToLower()[0];
		//	
		////					if(myFont.KeyValid(ch))
		////					{
		////						if(shift)
		////							command+=myFont.ShiftValue(ch);
		////						else
		////							command+=ch;
		////					}
		////					else
		////					{
		////						ch = e.KeyCode.ToString()[0];
		////						System.Console.WriteLine(e.KeyCode.ToString());
		////						if(myFont.KeyValid(ch))
		////						{
		////							if(shift)
		////								command+=myFont.ShiftValue(ch);
		////							else
		////								command+=ch;
		////						}
		////					}
		//					break;
		//			}
		//		}
		//
		//		public void KeyUp(object sender, KeyEventArgs e)
		//		{
		//			switch(e.KeyCode)
		//			{
		//				case Keys.ShiftKey:
		//					shift=false;
		//					break;
		//			}
		//		}
	}

	public class Node
	{
		public Node last;
		public Node next;
		public string str;

		public Node(string str)
		{
			next=null;
			this.str=str;
		}
	}
}