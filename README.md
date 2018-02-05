# Her skruer vi Dinero.dk op på 11
Måske kender du http://dinero.dk?
Dinero er et virkelig godt og simpelt regnskabs program/service, til små virksomheder.

Dinero kan rigtig mange ting, men de kan naturligvis ikke implementere alle features og ønsker. Derfor har jeg lavet dette repo med små tools, som jeg personlig har brug for og savner i Dinero. 

Forhåbentlig kan udviklerne ved Dinero så blive inspireret her, så vi kan få nogle fede features ind i Dinero :)

# Find de manglende bilag i Dinero.dk ved bankafstemning
Den stabile 'master' branch deployes her: http://dinero-difference.laumania.net/

Udviklings 'dev' branch deployes her: http://dinero-difference-tool-dev.azurewebsites.net/

Når du laver regnskab har du nogen gange en difference mellem hvad Dinero mener der skal stå i din bank, og hvad din bank reelt siger. Jo flere bilag du har, jo sværer er det at finde frem til hvor denne difference kommer fra.

Det er det problem det her tool forsøger at hjælpe med.

Hvis du vil gøre det manualet kan det gøres således:
https://dinero.dk/support/bankafstemning-hvordan-afstemmer-jeg-banken/

# Forslag eller fejl?
Finder du fejl, mangler eller har du en ide til noget der mangler, så opret et issue her på Github:
https://github.com/Laumania/Dinero.Tools/issues

Alternativt, så deltag i chatten, så tager vi en snak der:

[![Join the chat at https://gitter.im/Laumania/Dinero.Tools](https://badges.gitter.im/Laumania/Dinero.Tools.svg)](https://gitter.im/Laumania/Dinero.Tools?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

# Udvikler?
Er du udvikler, skal du være velkommen til at sende pull requests med forslag :)

# Tekniske spidsfindigheder
- Visual Studio 2017
- C# .NET 4.5.2
- ASP.NET MVC 5
- UnitTest
- FileHelpers .NET (Lib til let at læse CSV filer)
- "Freelancer" Bootstrap theme - http://startbootstrap.com/template-overviews/freelancer/
