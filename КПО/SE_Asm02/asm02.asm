.586
.MODEl flat, stdcall
includelib kernel32.lib

ExitProcess PROTO : DWORD
MessageBoxA PROTO : DWORD, : DWORD, : DWORD, : DWORD

.STACK 4096

.DATA 
MB_OK EQU 0
STR1 DB "Сложение", 0
STR2 DB "Результат сложения = 0", 0
NUM1 DD 3
NUM2 DD 4
HW   DD ?

.CODE
main PROC
START:

mov eax, [NUM1]
add eax, [NUM2]
add al, '0'
mov [STR2 + 21], al

push MB_OK
push OFFSET STR1
push OFFSET STR2
push HW
CALL MessageBoxA

push 0
call ExitProcess

main ENDP
end main