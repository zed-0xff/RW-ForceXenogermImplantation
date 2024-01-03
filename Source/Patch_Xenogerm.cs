using HarmonyLib;
using Verse;
using RimWorld;

namespace ForceXenogermImlpantation
{
    [HarmonyPatch(typeof(Xenogerm), nameof(Xenogerm.PawnIdeoDisallowsImplanting))]
    static class Patch_ForceXenogermImlpantation
    {
        static void Postfix(Xenogerm __instance, ref bool __result, Pawn selPawn)
        {
            if( (selPawn.IsSlaveOfColony || selPawn.IsPrisonerOfColony) && __result ){
                __result = false;
            }
        }
    }

    // Method body is basically the same as PawnIdeoDisallowsImplanting, but this one requires two parameters for checks against both the implanter and implantee
    [HarmonyPatch(typeof(CompAbilityEffect_ReimplantXenogerm), nameof(CompAbilityEffect_ReimplantXenogerm.PawnIdeoCanAcceptReimplant))]
    static class Patch_ForceReimplantXenogerm
    {
        static void Postfix(CompAbilityEffect_ReimplantXenogerm __instance, ref bool __result, Pawn implantee)
        {
            // This method checks for a true value unlike PawnIdeoDisallowsImplanting which checks for false
            if( (implantee.IsSlaveOfColony || implantee.IsPrisonerOfColony) && !__result ){
                __result = true;
            }
        }
    }
}