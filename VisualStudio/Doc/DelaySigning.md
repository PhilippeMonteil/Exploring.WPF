
# [Strong-named assemblies](https://docs.microsoft.com/en-us/dotnet/standard/assembly/strong-named)

## [Delay-sign an assembly](https://docs.microsoft.com/en-us/dotnet/standard/assembly/delay-sign)

## [Sn.exe (Strong Name Tool)](https://docs.microsoft.com/en-us/dotnet/framework/tools/sn-exe-strong-name-tool)

### Generating a signing key

	sn.exe -k mykey.snk
	sn.exe -p mykey.snk mykey.public.snk

### V�rification du 'Strong Name' d'une assembly

	sn -vf ClassLibrary1.dll

indique si l'assembly est sign�e, en cours de signature ou non sign�e.

### [Ildasm.exe (IL Disassembler)](https://docs.microsoft.com/en-us/dotnet/framework/tools/ildasm-exe-il-disassembler)

Visualisation du manifeste d'un assembly : liste de ses r�f�rences, sign�es ou non.
