// log

// 9/21/21
// xmovement
// xaiming
// projectile is combined with straight since they are typically the same, the straight line one is actually a projectile
	// xdestroy spells
	// xlimit range
// added aoe that allows the player to choose specific location to cast the spell in a certain range, might come in handy
	// xrestrict range

// 9/22/21
// damage
// control
// recovery

// 9/25/21
// xmaterial framework 1.0
	// materials are prefabs
	// each mat prefab can have multiple effect structs
	// effect struct = effect the mat gives to the spell
	// there can be only one material to be activated
	// combination of materials functions as one new material

// 9/30/21
// adjustments
	// projectile doesn't use gravity for now
	
// features
	// collision detection
	// xprojectile
		// xhave place to insert effect codes
	// xaoe
		// xspawn a transparent cyllinder to indicate collider
		// xscale aoe indicator according to the size of the collider
		// xhave place to insert effect codes
		// xfix the bug where no collision is detected
	// xpie
		// xuse angle to check if hit
		// xchange pie range indicator accordingly
		// xfind place to insert effect codes
	// xtarget
		// xfind place to insert effect codes

// 10/3/21
// adjustments
	// xnew .gitignore
	// xget current mat in hit detection and call effect from effect manager based on the mat's effects
	// xdamage
		// xone time damage
		// dot
	// xctrl
		// xwalkability
		// xatackability
		// foece move
	// xrecovery
		// xone time heal
		// hot
	// xsupply
		// xdrop mats

// 10/9/21
// xcombination
	// xput combat into level
// foce move
	// ximpulse knock back
	// knock back enemy
	// xknock back player

// 10/21/21
// combat refine
	// drop mat system change
		// xaccumulated, not instant
		// xui
	// skill cast system change
		// xwindup and backswing
		// GP
		// deal damage visual tell
	