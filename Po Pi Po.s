	.include "MPlayDef.s"

	.equ	Po_Pi_Po_grp, voicegroup000
	.equ	Po_Pi_Po_pri, 0
	.equ	Po_Pi_Po_rev, 0
	.equ	Po_Pi_Po_mvl, 127
	.equ	Po_Pi_Po_key, 0
	.equ	Po_Pi_Po_tbs, 1
	.equ	Po_Pi_Po_exg, 0
	.equ	Po_Pi_Po_cmp, 1

	.section .rodata
	.global	Po_Pi_Po
	.align	2

@**************** Track 1 (Midi-Chn.1) ****************@

Po_Pi_Po_1:
	.byte	KEYSH , Po_Pi_Po_key+0
@ 000   ----------------------------------------
	.byte	TEMPO , 113*Po_Pi_Po_tbs/2
	.byte		VOICE , 56
	.byte		VOL   , 100*Po_Pi_Po_mvl/mxv
	.byte		PAN   , c_v+0
	.byte		N11   , Cn4 , v080
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Dn4
	.byte	W12
	.byte		        Gn4
	.byte	W12
	.byte		N23   , Dn4
	.byte	W24
@ 001   ----------------------------------------
Po_Pi_Po_1_001:
	.byte		N11   , Cn4 , v080
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Dn4
	.byte	W12
	.byte		        Gn4
	.byte	W12
	.byte		N23   , Dn4
	.byte	W24
	.byte	PEND
@ 002   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 003   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 004   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 005   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 006   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 007   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 008   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 009   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 010   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 011   ----------------------------------------
	.byte		N11   , Cn4 , v080
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Gn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Dn4
	.byte	W12
	.byte		        Gn4
	.byte	W36
@ 012   ----------------------------------------
Po_Pi_Po_1_012:
	.byte		N11   , Gn3 , v080
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Gn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Gn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Gn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Gn3
	.byte		N05   , Bn3
	.byte	W06
	.byte		N11   , Gn3
	.byte	W12
	.byte		N17   , Bn2
	.byte		N17   , En3
	.byte	W18
	.byte		N11   , Gn3
	.byte		N11   , Dn4
	.byte	W12
	.byte	PEND
@ 013   ----------------------------------------
Po_Pi_Po_1_013:
	.byte		N11   , An3 , v080
	.byte		N11   , Cn4
	.byte	W12
	.byte		        An3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        An3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        An3
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Fn3
	.byte		N05   , Bn3
	.byte	W06
	.byte		N11   , Cn4
	.byte	W12
	.byte		N17   , Gn3
	.byte		N17   , Dn4
	.byte	W18
	.byte		N11   , Gn3
	.byte		N11   , En4
	.byte	W12
	.byte	PEND
@ 014   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 015   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 016   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 017   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 018   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 019   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 020   ----------------------------------------
	.byte		N11   , Cn4 , v080
	.byte		N11   , En4
	.byte	W12
	.byte		N05   , Dn4
	.byte	W06
	.byte		N17   , Cn4
	.byte		N17   , En4
	.byte	W18
	.byte		N05   , Cn4
	.byte		N05   , En4
	.byte	W06
	.byte		        Fn4
	.byte	W06
	.byte		        En4
	.byte		N05   , Gn4
	.byte	W06
	.byte		        An4
	.byte	W06
	.byte		        En4
	.byte		N05   , Gn4
	.byte	W06
	.byte		N17
	.byte	W18
	.byte		N11   , Bn3
	.byte		N11   , Fn4
	.byte	W12
@ 021   ----------------------------------------
Po_Pi_Po_1_021:
	.byte		N11   , An3 , v080
	.byte		N11   , En4
	.byte	W12
	.byte		        Fn4
	.byte	W12
	.byte		        An3
	.byte		N11   , En4
	.byte	W12
	.byte		        Fn4
	.byte	W12
	.byte		N23   , Fn3
	.byte		N23   , An3
	.byte		N23   , En4
	.byte	W24
	.byte		        Gn3
	.byte		N23   , Bn3
	.byte	W24
	.byte	PEND
@ 022   ----------------------------------------
	.byte		N11   , Cn4
	.byte		N11   , En4
	.byte	W12
	.byte		        Cn4
	.byte		N11   , En4
	.byte	W12
	.byte		        Cn4
	.byte		N11   , En4
	.byte	W12
	.byte		        Fn4
	.byte	W12
	.byte		N05   , En4
	.byte		N05   , Gn4
	.byte	W06
	.byte		N11   , An4
	.byte	W12
	.byte		N17   , En4
	.byte		N17   , Gn4
	.byte	W18
	.byte		N11   , Fn4
	.byte	W12
@ 023   ----------------------------------------
	.byte		        An3
	.byte		N11   , En4
	.byte	W12
	.byte		        Fn4
	.byte	W12
	.byte		        An3
	.byte		N11   , En4
	.byte	W12
	.byte		N44   , Gn3
	.byte		N44   , Cn4
	.byte	W48
	.byte		N05   , Gn3
	.byte	W06
	.byte		N05
	.byte	W06
@ 024   ----------------------------------------
	.byte		N11   , Cn4
	.byte	W12
	.byte		N23
	.byte		N23   , En4
	.byte	W24
	.byte		N11   , Fn4
	.byte	W12
	.byte		        En4
	.byte		N11   , Gn4
	.byte	W12
	.byte		N05   , An4
	.byte	W06
	.byte		N17   , En4
	.byte		N17   , Gn4
	.byte	W18
	.byte		N11   , Fn4
	.byte	W12
@ 025   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_021
@ 026   ----------------------------------------
	.byte		N11   , Cn4 , v080
	.byte		N11   , En4
	.byte	W12
	.byte		        Cn4
	.byte		N11   , En4
	.byte	W12
	.byte		        Cn4
	.byte		N11   , En4
	.byte	W12
	.byte		        Dn4
	.byte		N11   , Fn4
	.byte	W12
	.byte		        En4
	.byte		N11   , Gn4
	.byte	W12
	.byte		        En4
	.byte		N11   , Cn5
	.byte	W12
	.byte		        En4
	.byte		N11   , Bn4
	.byte	W12
	.byte		TIE   , En4
	.byte		TIE   , Cn5
	.byte	W12
@ 027   ----------------------------------------
	.byte	W92
	.byte	W03
	.byte		EOT   , En4
	.byte		        Cn5
	.byte	W01
@ 028   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 029   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 030   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 031   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 032   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 033   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 034   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_012
@ 035   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_013
@ 036   ----------------------------------------
Po_Pi_Po_1_036:
	.byte		N23   , Cn3 , v080
	.byte		N23   , En3
	.byte	W24
	.byte		        Cn3
	.byte	W24
	.byte		N11   , Bn2
	.byte		N11   , Dn3
	.byte	W12
	.byte		N23   , Gn3
	.byte	W24
	.byte		N11   , Bn2
	.byte		N11   , Fn3
	.byte	W12
	.byte	PEND
@ 037   ----------------------------------------
	.byte		        Cn3
	.byte		N11   , En3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , En3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		N23   , An2
	.byte		N23   , En3
	.byte	W24
	.byte		        Bn2
	.byte		N23   , Dn3
	.byte	W24
@ 038   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_036
@ 039   ----------------------------------------
	.byte		N11   , Cn3 , v080
	.byte		N11   , En3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , En3
	.byte	W12
	.byte		        Dn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		N44   , En3
	.byte		N44   , Gn3
	.byte	W48
@ 040   ----------------------------------------
	.byte		N23   , Gn2
	.byte		N23   , Cn3
	.byte	W24
	.byte		        En3
	.byte		N23   , Cn4
	.byte	W24
	.byte		N11   , En3
	.byte		N11   , Bn3
	.byte	W12
	.byte		N23   , Gn3
	.byte	W24
	.byte		N11   , Fn3
	.byte	W12
@ 041   ----------------------------------------
	.byte		        Cn3
	.byte		N11   , En3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , En3
	.byte	W12
	.byte		        Dn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		N23   , En3
	.byte		N23   , Gn3
	.byte	W24
	.byte		        Gn2
	.byte		N23   , Cn3
	.byte	W24
@ 042   ----------------------------------------
	.byte	W24
	.byte		N11
	.byte		N11   , En3
	.byte	W12
	.byte		        Cn3
	.byte		N11   , Fn3
	.byte	W12
	.byte		        En3
	.byte		N11   , Gn3
	.byte	W12
	.byte		        En3
	.byte		N11   , Cn4
	.byte	W12
	.byte		N23   , En3
	.byte		N23   , Bn3
	.byte	W24
@ 043   ----------------------------------------
	.byte		N92   , An3
	.byte		N92   , Cn4
	.byte	W96
@ 044   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 045   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 046   ----------------------------------------
Po_Pi_Po_1_046:
	.byte		N23   , Cn3 , v080
	.byte		N23   , En3
	.byte	W24
	.byte		        Gn3
	.byte		N23   , Cn4
	.byte	W24
	.byte		N11   , Gn3
	.byte		N11   , Dn4
	.byte	W12
	.byte		        Gn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Gn3
	.byte		N11   , Bn3
	.byte	W12
	.byte		        Gn3
	.byte	W12
	.byte	PEND
@ 047   ----------------------------------------
Po_Pi_Po_1_047:
	.byte		N11   , An3 , v080
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , An3
	.byte		N05   , Cn4
	.byte	W06
	.byte		N17   , An3
	.byte		N17   , Cn4
	.byte	W18
	.byte		N11   , An3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Fn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		        Fn3
	.byte		N11   , Cn4
	.byte	W12
	.byte		N05   , Gn3
	.byte		N05   , Cn4
	.byte	W06
	.byte		        Gn3
	.byte		N05   , Cn4
	.byte	W06
	.byte		N11   , Gn3
	.byte		N11   , Cn4
	.byte	W12
	.byte	PEND
@ 048   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 049   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 050   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_046
@ 051   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_047
@ 052   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 053   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 054   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_046
@ 055   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_047
@ 056   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 057   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_1_001
@ 058   ----------------------------------------
	.byte		N23   , Gn3 , v080
	.byte		N23   , Cn4
	.byte	W24
	.byte		        Gn3
	.byte		N23   , Dn4
	.byte	W24
	.byte		        Gn3
	.byte		N23   , En4
	.byte	W24
	.byte		        Gn3
	.byte		N23   , Fn4
	.byte	W24
@ 059   ----------------------------------------
	.byte		N11   , En4
	.byte		N11   , Gn4
	.byte	W12
	.byte		N05   , Fn4
	.byte	W06
	.byte		N23   , En4
	.byte	W30
	.byte		N11   , Gn3
	.byte		N11   , Dn4
	.byte	W12
	.byte		N05   , Cn4
	.byte	W06
	.byte		N17   , Gn3
	.byte		N17   , Bn3
	.byte	W18
	.byte		N11
	.byte		N11   , Dn4
	.byte	W12
@ 060   ----------------------------------------
	.byte		N92   , Cn4
	.byte	W92
	.byte	W03
	.byte	GOTO
	.word	Po_Pi_Po_1
	.byte	W06
	.byte	FINE

@**************** Track 2 (Midi-Chn.2) ****************@

Po_Pi_Po_2:
	.byte		VOL   , 127*Po_Pi_Po_mvl/mxv
	.byte	KEYSH , Po_Pi_Po_key+0
@ 000   ----------------------------------------
	.byte		VOICE , 48
	.byte		N44   , Cn3 , v080
	.byte	W48
	.byte		N44
	.byte	W48
@ 001   ----------------------------------------
Po_Pi_Po_2_001:
	.byte		N44   , Cn3 , v080
	.byte	W48
	.byte		N44
	.byte	W48
	.byte	PEND
@ 002   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_001
@ 003   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_001
@ 004   ----------------------------------------
Po_Pi_Po_2_004:
	.byte		N11   , En2 , v080
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Fn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Cn3
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Gn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte	PEND
@ 005   ----------------------------------------
Po_Pi_Po_2_005:
	.byte		N11   , En2 , v080
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Fn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Cn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Gn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte	PEND
@ 006   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_004
@ 007   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_005
@ 008   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_004
@ 009   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_005
@ 010   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_004
@ 011   ----------------------------------------
	.byte		N11   , En2 , v080
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Fn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		        Cn2
	.byte	W12
	.byte		N11
	.byte	W12
	.byte		N23   , Gn2
	.byte	W24
@ 012   ----------------------------------------
Po_Pi_Po_2_012:
	.byte		N11   , Cn1 , v080
	.byte	W12
	.byte		        Cn2
	.byte	W12
	.byte		        Cn1
	.byte	W12
	.byte		        Cn2
	.byte	W12
	.byte		        En1
	.byte	W12
	.byte		        En2
	.byte	W12
	.byte		        En1
	.byte	W12
	.byte		        En2
	.byte	W12
	.byte	PEND
@ 013   ----------------------------------------
Po_Pi_Po_2_013:
	.byte		N11   , An1 , v080
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		        An1
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		        Fn1
	.byte	W12
	.byte		        Fn2
	.byte	W12
	.byte		        Gn1
	.byte	W12
	.byte		        Gn2
	.byte	W12
	.byte	PEND
@ 014   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 015   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 016   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 017   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 018   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 019   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 020   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 021   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 022   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 023   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 024   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 025   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 026   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 027   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 028   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 029   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 030   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 031   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 032   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 033   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 034   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 035   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 036   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 037   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 038   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 039   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 040   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 041   ----------------------------------------
	.byte		N11   , An1 , v080
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		        An1
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		        Fn1
	.byte	W12
	.byte		        Fn2
	.byte	W12
	.byte		N23   , Gn1
	.byte	W24
@ 042   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 043   ----------------------------------------
	.byte		N11   , An1 , v080
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		        An1
	.byte	W12
	.byte		        An2
	.byte	W12
	.byte		N15   , Gn1
	.byte		N15   , Dn2
	.byte	W16
	.byte		        Gn1
	.byte		N15   , Dn2
	.byte	W16
	.byte		        Gn1
	.byte		N15   , Dn2
	.byte	W16
@ 044   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 045   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 046   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 047   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 048   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 049   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 050   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 051   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 052   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 053   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 054   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 055   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 056   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 057   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 058   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_012
@ 059   ----------------------------------------
	.byte	PATT
	 .word	Po_Pi_Po_2_013
@ 060   ----------------------------------------
	.byte		N23   , Gn2 , v080
	.byte	W24
	.byte		        Fn2
	.byte	W24
	.byte		        En2
	.byte	W24
	.byte		        Cn2
	.byte	W23
	.byte	GOTO
	.word	Po_Pi_Po_2
	.byte	W06
	.byte	FINE

@******************************************************@
	.align	2

Po_Pi_Po:
	.byte	2	@ NumTrks
	.byte	0	@ NumBlks
	.byte	Po_Pi_Po_pri	@ Priority
	.byte	Po_Pi_Po_rev	@ Reverb.

	.word	Po_Pi_Po_grp

	.word	Po_Pi_Po_1
	.word	Po_Pi_Po_2

	.end
