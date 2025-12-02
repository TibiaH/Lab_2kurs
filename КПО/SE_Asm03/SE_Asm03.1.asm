.586
.MODEL flat, stdcall

.DATA
array DWORD 5, -2, 0, 8, 3, -1, 4

.CODE
main PROC
mov EAX, 0
mov ESI, 0
mov ECX, 7

sum_loop:
add EAX, array[ESi*4]
inc ESI
loop sum_loop

mov EBX, 1
mov ESI, 0
mov ECX, 7

check_zero_loop:
cmp array[ESI*4], 0
jne not_zero
mov EBX, 0
jmp end_check

not_zero:
inc ESI
loop check_zero_loop

end_check:

main ENDP
end main