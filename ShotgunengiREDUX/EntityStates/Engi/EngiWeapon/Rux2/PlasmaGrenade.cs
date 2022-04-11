using System;
using EngiShotgu;
using EntityStates.EngiTurret.EngiTurretWeapon;
using EntityStates.Toolbot;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon.Rux2
{
	// Token: 0x02000003 RID: 3
	public class PlasmaGrenade : AimThrowableBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002104 File Offset: 0x00000304
		public override void OnEnter()
		{
			if (this.goodState == null)
			{
				this.goodState = new AimStunDrone();
			}
			this.maxDistance = 60f;
			this.rayRadius = 8.51f;
			this.arcVisualizerPrefab = this.goodState.arcVisualizerPrefab;
			this.projectilePrefab = Engiplugin.PlasmaGrenadeObject;
			this.endpointVisualizerPrefab = this.goodState.endpointVisualizerPrefab;
			this.endpointVisualizerRadiusScale = 4f;
			this.setFuse = false;
			this.damageCoefficient = 6f;
			this.baseMinimumDuration = 0.1f;
			this.projectileBaseSpeed = 35f;
			base.OnEnter();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A4 File Offset: 0x000003A4
		public override void FixedUpdate()
		{
			base.characterBody.SetAimTimer(0.25f);
			base.fixedAge += Time.fixedDeltaTime;
			bool flag = false;
			if (base.isAuthority && !this.KeyIsDown() && base.fixedAge >= this.minimumDuration)
			{
				flag = true;
			}
			if (base.characterBody && base.characterBody.isSprinting)
			{
				flag = true;
			}
			if (flag)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000222C File Offset: 0x0000042C
		public override void OnExit()
		{
			base.OnExit();
			base.PlayCrossfade("Gesture, Additive", "FireMineRight", "FireMine.playbackRate", 0.3f, 0.05f);
			base.AddRecoil(0f, 0f, 0f, 0f);
			base.characterBody.AddSpreadBloom(1.75f);
			Util.PlaySound("Play_commando_M2_grenade_throw", base.gameObject);
			EffectManager.SimpleMuzzleFlash(FireGauss.effectPrefab, base.gameObject, PlasmaGrenade.muzzleString, false);
		}

		// Token: 0x04000003 RID: 3
		private AimStunDrone goodState;

		// Token: 0x04000004 RID: 4
		public static string muzzleString = "MuzzleCenter";
	}
}
