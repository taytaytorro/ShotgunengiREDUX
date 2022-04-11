using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ShotgunengiREDUX.Properties
{
	// Token: 0x02000002 RID: 2
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("ShotgunengiREDUX.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002087 File Offset: 0x00000287
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208E File Offset: 0x0000028E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002096 File Offset: 0x00000296
		internal static byte[] engishotgunassets
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("engishotgunassets", Resources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B1 File Offset: 0x000002B1
		internal static byte[] engishotgunicon
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("engishotgunicon", Resources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		internal static byte[] grenade1
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("grenade1", Resources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E7 File Offset: 0x000002E7
		internal static byte[] Image1
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("Image1", Resources.resourceCulture);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager resourceMan;

		// Token: 0x04000002 RID: 2
		private static CultureInfo resourceCulture;
	}
}
