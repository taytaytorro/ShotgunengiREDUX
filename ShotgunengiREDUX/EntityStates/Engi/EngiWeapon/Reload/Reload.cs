using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon.Reload
{
	// Token: 0x02000005 RID: 5
	public class Reload : BaseState
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000026A2 File Offset: 0x000008A2
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = base.skillLocator.secondary.CalculateFinalRechargeInterval();
			this.hasReloaded = false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026C8 File Offset: 0x000008C8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= 0.7f * this.duration)
			{
				this.StartReload();
			}
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002718 File Offset: 0x00000918
		private void StartReload()
		{
			if (!this.hasReloaded)
			{
				this.hasReloaded = true;
				base.PlayAnimation("Gesture, Additive", (base.characterBody.isSprinting && base.characterMotor && base.characterMotor.isGrounded) ? "ChargeGrenades" : "Reload", "Reload.playbackRate", this.duration);
				Util.PlayAttackSpeedSound(Reload.enterSoundString, base.gameObject, Reload.enterSoundPitch);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002793 File Offset: 0x00000993
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000279B File Offset: 0x0000099B
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return 0;
		}

		// Token: 0x04000017 RID: 23
		private float duration;

		// Token: 0x04000018 RID: 24
		private bool hasReloaded;

		// Token: 0x04000019 RID: 25
		public static float enterSoundPitch;

		// Token: 0x0400001A RID: 26
		public static float exitSoundPitch;

		// Token: 0x0400001B RID: 27
		public static string enterSoundString;

		// Token: 0x0400001C RID: 28
		public static string exitSoundString;

		// Token: 0x0400001D RID: 29
		public static GameObject reloadEffectPrefab;

		// Token: 0x0400001E RID: 30
		public static string reloadEffectMuzzleString;

		// Token: 0x0400001F RID: 31
		public static float baseDuration;
	}
}
