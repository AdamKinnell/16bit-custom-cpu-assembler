li $t0 1        # $t0 = 1

add $t0 1       # do { $t0++ }
clt $t0 10      # while ($t0 < 10)
brc 0x4         #

halt            # $t0 is now 10