using System;
using System.IO;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;
using System.Collections.Generic;

namespace XCom
{
	/// <summary>
	/// Summary description for BDYImage.
	/// </summary>
	public class BDYImage:XCImage
	{
		public BDYImage(Palette p,Stream s,int width, int height)
		{
			BinaryReader data = new BinaryReader(s);

			idx = new byte[width*height];
			for(int i=0;i<idx.Length;i++)
				idx[i]=254;

			int x = 0;

			while(data.BaseStream.Position<data.BaseStream.Length)
			{
				int space = data.ReadByte();
				byte c = data.ReadByte();

				if(space>=129)
				{
					space = 256-space+1;
					for(int i=0;i<space;i++)
						idx[x++]=c;
				}
				else
				{
					idx[x++]=c;
					for(int i=0;i<space;i++)
					{
						c=data.ReadByte();
						idx[x++]=c;
					}
				}
			}
			image = Bmp.MakeBitmap8(320,200,idx,p.Colors);
			Palette=p;

			data.Close();
		}

		public override byte TransparentIndex{get{return 0;}}

		public static void Save(byte[] img,Stream file)
		{
			//int transparent=0;
			BinaryWriter data = new BinaryWriter(file);

			//ArrayList al = new ArrayList();
			List<BdyNode> al = new List<BdyNode>();

			BdyNode last = new BdyNode(img[0]);
			al.Add(last);
			int count=0;
			for(int i=1;i<img.Length;i++)
			{
				count++;
				BdyNode curr = new BdyNode(img[i]);
				
				if(count%320==0)
				{
					last=curr;
					al.Add(last);
					count=0;
					continue;
				}

				if(curr.data==last.data) //we have a match
				{
					if(last.count<128)
						last.count++;
					else
					{
						last=curr;
						al.Add(last);
					}
				}
				else
				{
					last=curr;
					al.Add(last);
				}
			}
			
			count=0;
			List<BdyNode> tmp = new List<BdyNode>();
			foreach(BdyNode bn in al)
			{
				if(bn.count==1)
					tmp.Add(bn);
				else if(bn.count==2 && tmp.Count!=0)
				{
					tmp.Add(bn);
					tmp.Add(bn);
				}
				else //write out whats in the array list, write out our value, reset arraylist
				{
					if(tmp.Count>0)
					{
						if(count+tmp.Count>=320)
						{
							int left = 320-count;
							if(left>0)
							{
								data.Write((byte)(left-1));
								for(int i=0;i<left;i++)
									data.Write((byte)tmp[i].data);
								int left2 = tmp.Count-left;
								if(left2>0)
								{
									data.Write((byte)(left2-1));
									for(int i=0;i<left2;i++)
										data.Write((byte)tmp[left+i].data);
								}
								count = left2;
							}
							else
							{
								data.Write((byte)(tmp.Count-1));
								count+=tmp.Count;
								for(int i=0;i<tmp.Count;i++)
									data.Write((byte)tmp[i].data);					
							}
						}
						else
						{
							data.Write((byte)(tmp.Count-1));
							count+=tmp.Count;
							for(int i=0;i<tmp.Count;i++)
								data.Write((byte)tmp[i].data);				
						}
						tmp = new List<BdyNode>();
					}

					data.Write((byte)(256-bn.count+1));
					count+=bn.count;
					data.Write(bn.data);

					if(count>320)
						count-=320;
				}
			}

			if(tmp.Count>0)
			{
				data.Write((byte)(tmp.Count-1));

				for(int i=0;i<tmp.Count;i++)
					data.Write(tmp[i].data);
				tmp = new List<BdyNode>();
			}

			data.Flush();
			data.Close();
		}
		
		//private enum BdyNodeType{DataOnly,RunLength};
		private class BdyNode
		{
			//public BdyNodeType myType;
			public byte data;
			public byte count;

			public BdyNode(byte data)
			{
				this.data=data;
				count=1;
				//myType=BdyNodeType.DataOnly;
			}

			public BdyNode(byte count,byte data)
			{
				this.count=count;
				this.data=data;
				//myType=BdyNodeType.RunLength;
			}
		}
	}
}