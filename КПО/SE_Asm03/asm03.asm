.586
.MODEL flat, stdcall

.DATA

myDoubles DWORD 1,2,3,4,5,6

.CODE
main PROC

mov ESI, 0
mov EAX, myDoubles[ESI]
mov EDX, [myDoubles + ESI]

main ENDP
end main