add $t0 $zero 1  # $t0 = 1

add  $t0 $t0 1   # do $t0++
clt  $t0 10      # while ($t0 < 10)
subc $pc 8       #

halt             # $t0 is now 10