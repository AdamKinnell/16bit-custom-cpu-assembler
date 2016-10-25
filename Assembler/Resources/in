li $t0 0           # $t0 = 0

                   # do {
add $t0 1          #   $t0++
sb  $t0, 0xFF($t0) #   *($t0) = $t0
clt $t0 10         # while ($t0 < 10)
brc 0x4            #

halt               # $t0 is now 10 ; values 1-10 stored in data memory.