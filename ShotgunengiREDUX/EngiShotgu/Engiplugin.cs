using System;
using System.Collections.Generic;
using BepInEx;
using EngiShotgun.Assets;
using EntityStates;
using EntityStates.Engi.EngiWeapon.Rux1;
using EntityStates.Engi.EngiWeapon.Rux2;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using ShotgunengiREDUX.Properties;
using UnityEngine;
using UnityEngine.Networking;

namespace EngiShotgu
{
	// Token: 0x02000007 RID: 7
	[BepInDependency("com.bepis.r2api")]
	[BepInPlugin("com.Ruxbieno.EngiShotgun", "EngiGamingREDUX", "1.0.1")]
	[R2APISubmoduleDependency(new string[]
	{
		"PrefabAPI",
		"SurvivorAPI",
		"LanguageAPI",
		"LoadoutAPI",
		"ItemAPI",
		"DifficultyAPI",
		"BuffAPI",
		"ProjectileAPI",
		"EffectAPI"
	})]
	public class Engiplugin : BaseUnityPlugin
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002808 File Offset: 0x00000A08
		public void Awake()
		{
			this.SetupProjectiles();
			SkillLocator component = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/EngiBody").GetComponent<SkillLocator>();
			SkillFamily skillFamily = component.primary.skillFamily;
			SkillFamily skillFamily2 = component.secondary.skillFamily;
			ContentAddition.AddEntityState<GaussShotgun>(out _);
			ReloadSkillDef reloadSkillDef = ScriptableObject.CreateInstance<ReloadSkillDef>();
			reloadSkillDef.skillDescriptionToken = "Fire a close-range blast of pellets, dealing <style=cIsDamage>8x60% damage</style>. Holds 6 total rounds.";
			reloadSkillDef.skillName = "EngiShotgun";
			reloadSkillDef.skillNameToken = "Gauss Scatter";
			reloadSkillDef.icon = Engiplugin.gaussShotgunIconS;
			reloadSkillDef.activationState = new SerializableEntityStateType(typeof(GaussShotgun));
			reloadSkillDef.activationStateMachineName = "Weapon";
			reloadSkillDef.baseMaxStock = this.stock;
			reloadSkillDef.baseRechargeInterval = 0.75f;
			reloadSkillDef.beginSkillCooldownOnSkillEnd = true;
			reloadSkillDef.canceledFromSprinting = false;
			reloadSkillDef.fullRestockOnAssign = true;
			reloadSkillDef.interruptPriority = 0;
			reloadSkillDef.resetCooldownTimerOnUse = true;
			reloadSkillDef.mustKeyPress = false;
			reloadSkillDef.isCombatSkill = true;
			reloadSkillDef.cancelSprintingOnActivation = true;
			reloadSkillDef.forceSprintDuringState = false;
			reloadSkillDef.rechargeStock = 1;
			reloadSkillDef.requiredStock = 1;
			reloadSkillDef.stockToConsume = 1;
			ContentAddition.AddSkillDef(reloadSkillDef);
			Array.Resize<SkillFamily.Variant>(ref skillFamily.variants, skillFamily.variants.Length + 1);
			SkillFamily.Variant[] variants = skillFamily.variants;
			int num = skillFamily.variants.Length - 1;
			SkillFamily.Variant variant = default(SkillFamily.Variant);
			variant.skillDef = reloadSkillDef;
			variant.unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
			variant.viewableNode = new ViewablesCatalog.Node(reloadSkillDef.skillNameToken, false, null);
			variants[num] = variant;
			ContentAddition.AddEntityState<PlasmaGrenade>(out _);
			SkillDef skillDef = ScriptableObject.CreateInstance<SkillDef>();
			skillDef.activationState = new SerializableEntityStateType(typeof(PlasmaGrenade));
			skillDef.activationStateMachineName = "Weapon";
			skillDef.baseMaxStock = 2;
			skillDef.baseRechargeInterval = 8f;
			skillDef.beginSkillCooldownOnSkillEnd = true;
			skillDef.canceledFromSprinting = true;
			skillDef.fullRestockOnAssign = false;
			skillDef.interruptPriority = EntityStates.InterruptPriority.Skill;
			skillDef.isCombatSkill = true;
			skillDef.mustKeyPress = true;
			skillDef.cancelSprintingOnActivation = true;
			skillDef.forceSprintDuringState = false;
			skillDef.rechargeStock = 1;
			skillDef.requiredStock = 1;
			skillDef.stockToConsume = 1;
			skillDef.icon = Engiplugin.plasmaGrenadeIconS;
			skillDef.skillDescriptionToken = "Take aim and lob a plasma grenade that deals <style=cIsDamage>600% damage</style> on impact and leaves a pool of plasma that deals <style=cIsDamage>30% damage per tick</style> and <style=cIsUtility>slows</style>. Can hold up to 2.";
			skillDef.skillName = "PlasmaGrenade";
			skillDef.skillNameToken = "Plasma Grenade";
			ContentAddition.AddSkillDef(skillDef);
			Array.Resize<SkillFamily.Variant>(ref skillFamily2.variants, skillFamily2.variants.Length + 1);
			SkillFamily.Variant[] variants2 = skillFamily2.variants;
			int num2 = skillFamily2.variants.Length - 1;
			variant = default(SkillFamily.Variant);
			variant.skillDef = skillDef;
			variant.unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
			variant.viewableNode = new ViewablesCatalog.Node(reloadSkillDef.skillNameToken, false, null);
			variants2[num2] = variant;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AA1 File Offset: 0x00000CA1
		private void SetupProjectiles()
		{
			this.SetupPlasmaGrenade();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void SetupPlasmaGrenade()
		{
			Engiplugin.PlasmaGrenadeObject = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/projectiles/banditgrenadeprojectile"), "PlasmaGrenade", true);
			this.PlasmaGrenadeGhostObject = PrefabAPI.InstantiateClone(Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileController>().ghostPrefab, "PlasmaGrenadeGhost", false);
			Engiplugin.projectilePrefabs.Add(Engiplugin.PlasmaGrenadeObject);
			Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileController>().ghostPrefab = this.PlasmaGrenadeGhostObject;
			Engiplugin.PlasmaGrenadeObject.AddComponent<NetworkBehaviour>();
			GameObject gameObject = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/projectiles/crocoleapacid"), "BasedPlasmaPuddle", true);
			Engiplugin.projectilePrefabs.Add(gameObject);
			base.gameObject.AddComponent<NetworkIdentity>();
			base.gameObject.AddComponent<NetworkBehaviour>();
			ProjectileDamage component = gameObject.GetComponent<ProjectileDamage>();
			component.damageType = DamageType.SlowOnHit;
			component.damage = 6f;
			component.force = 30f;
			ProjectileDotZone component2 = gameObject.GetComponent<ProjectileDotZone>();
			component2.attackerFiltering = 0;
			component2.overlapProcCoefficient = 0.15f;
			component2.lifetime = 5f;
			component2.damageCoefficient = 0.3f;
			GameObject gameObject2 = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/effects/impacteffects/engimineexplosion"), "PlasmaGrenadeBoomEffect", false);
			gameObject2.GetComponent<EffectComponent>().soundName = "Play_acrid_shift_land";
			gameObject2.AddComponent<NetworkIdentity>();
			gameObject2.AddComponent<NetworkBehaviour>();
			Engiplugin.effectDefs.Add(new EffectDef(gameObject2));
			Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileSimple>().desiredForwardSpeed = 60f;
			ProjectileImpactExplosion component3 = Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileImpactExplosion>();
			component3.blastRadius = 8.51f;
			Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileDamage>().damageType = DamageType.SlowOnHit;
			component3.blastProcCoefficient = 1f;
			component3.falloffModel = 0;
			component3.lifetime = 20f;
			component3.impactEffect = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/impacteffects/engimineexplosion");
			component3.explosionSoundString = "Play_engi_M2_explo";
			component3.timerAfterImpact = false;
			component3.lifetimeAfterImpact = 0f;
			component3.destroyOnWorld = true;
			component3.destroyOnEnemy = true;
			component3.fireChildren = true;
			component3.childrenCount = 1;
			component3.childrenDamageCoefficient = 0.3f;
			component3.childrenProjectilePrefab = gameObject;
			UnityEngine.Object.Destroy(Engiplugin.PlasmaGrenadeObject.GetComponent<ProjectileStickOnImpact>());
			Engiplugin.PlasmaGrenadeObject.GetComponent<Rigidbody>().useGravity = true;
			this.PlasmaGrenadeGhostObject.AddComponent<NetworkIdentity>();
			this.PlasmaGrenadeGhostObject.AddComponent<NetworkBehaviour>();
			ContentAddition.AddProjectile(Engiplugin.PlasmaGrenadeObject);
		}

		// Token: 0x04000020 RID: 32
		public InterruptPriority reloadInterruptPriority = EntityStates.InterruptPriority.Skill;

		// Token: 0x04000021 RID: 33
		public float graceDuration;

		// Token: 0x04000022 RID: 34
		public SerializableEntityStateType reloadState;

		// Token: 0x04000023 RID: 35
		public int stock = 6;

		// Token: 0x04000024 RID: 36
		public static Texture2D gaussShotgunIcon = Assets.LoadTexture2D(ShotgunengiREDUX.Properties.Resources.engishotgunicon);

		// Token: 0x04000025 RID: 37
		public static Sprite gaussShotgunIconS = Assets.TexToSprite(Engiplugin.gaussShotgunIcon);

		// Token: 0x04000026 RID: 38
		public static Texture2D plasmaGrenadeIcon = Assets.LoadTexture2D(ShotgunengiREDUX.Properties.Resources.grenade1);

		// Token: 0x04000027 RID: 39
		public static Sprite plasmaGrenadeIconS = Assets.TexToSprite(Engiplugin.plasmaGrenadeIcon);

		// Token: 0x04000028 RID: 40
		public static GameObject PlasmaGrenadeObject;

		// Token: 0x04000029 RID: 41
		public GameObject PlasmaGrenadeGhostObject;

		// Token: 0x0400002A RID: 42
		public static List<GameObject> projectilePrefabs = new List<GameObject>();

		// Token: 0x0400002B RID: 43
		public static List<EffectDef> effectDefs = new List<EffectDef>();

		// Token: 0x0400002C RID: 44
		public static GameObject projectilePrefab;
	}
}
