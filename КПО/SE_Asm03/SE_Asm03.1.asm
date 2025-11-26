.586
.MODEL flat, stdcall

.DATA
array DWORD 5, -2, 0, 8, 3, -1, 4

.CODE
main PROC

mov EAX, array[0]
add EAX, array[4]
add EAX, array[8]
add EAX, array[12]
add EAX, array[16]
add EAX, array[20]
add EAX, array[24]

mov EBX, 1
cmp array[0], 0
je zero_found
cmp array[4], 0
je zero_found
cmp array[8], 0
je zero_found
cmp array[12], 0
je zero_found
cmp array[16], 0
je zero_found
cmp array[20], 0
je zero_found
cmp array[24], 0
jne no_zero

zero_found:
mov EBX, 0

no_zero:
main ENDP
end main