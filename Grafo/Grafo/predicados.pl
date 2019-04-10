%Amizades (User1, User2, Peso da Relação)
ligacao(1,6,5).
ligacao(6,1,5).
ligacao(1,2,8).
ligacao(2,1,8).
ligacao(2,3,10).
ligacao(3,2,10).
ligacao(2,6,1).
ligacao(6,2,1).
ligacao(3,4,2).
ligacao(4,3,2).
ligacao(4,5,4).
ligacao(5,4,4).
ligacao(6,7,1).
ligacao(7,6,1).
ligacao(7,8,6).
ligacao(8,7,6).
ligacao(1,9,3).
ligacao(9,1,3).
ligacao(9,10,4).
ligacao(10,9,4).
ligacao(10,11,5).
ligacao(11,10,5).
ligacao(10,7,6).
ligacao(7,10,6).
ligacao(7,9,10).
ligacao(9,7,10).
ligacao(5,7,3).
ligacao(7,5,3).

%tags (User, Lista de Tags)
tags(1,[musica,video,liberdade,porto,benfica]).
tags(2,[pintura,video,liberdade,porto]).
tags(6,[musica, dança,liberdade,porto,benfica]).
tags(9,[mundo,natureza,multimedia,braga,oculos,porto]).

%Ajuda
%Encontra o próximo amigo (User, Amigo do User)
amigos(X,Y):-ligacao(X,Y,_),ligacao(Y,X,_).
amigos(X,Y,W):-ligacao(X,Y,W),ligacao(Y,X,W).

%Todos os amigos de 1 pessoa (User, Lista de Amigos)
amigosLista(X,L):-findall(Y,amigos(X,Y),L).

%Verifica se dado 1 pessoa, Encontra o próximo amigo e se este faz parte de uma lista ou não (User, Lista, Amigo do User)
amigosLista(X,T,Z):-amigos(X,Z,_),not member(Z,T).
amigosLista(X,T,Z,C):-amigos(X,Z,C),not member(Z,T).

%Inverte 1 lista (Lista, Lista Invertida)
inverte(L,LI):-inverte(L,[],LI).
inverte([],LI,LI).
inverte([H|T],L,LI):-inverte(T,[H|L],LI).

%União de 2 listas (Lista1, Lista2, Lista Resultado)
uniao([ ],L,L).
uniao([X|L1],L2,LU):-member(X,L2),!,uniao(L1,L2,LU).
uniao([X|L1],L2,[X|LU]):-uniao(L1,L2,LU).

%Intersecção entre 2 listas (Lista1, Lista2, Lista Resultado)
interseccao([ ],_,[ ]). 
interseccao([X|L1],L2,[X|LI]):-member(X,L2),!,interseccao(L1,L2,LI). 
interseccao([_|L1],L2, LI):- interseccao(L1,L2,LI).

%Junta listas dentro de listas (Lista, Lista Resultado)
juntaListas([H|T],FL):-juntaListas([H|T],[],FL).
juntaListas([],FL,FL):-!.
juntaListas([H|T],ML,FL):-juntaListas(T,ML,NL),!,juntaListas(H,NL,FL).
juntaListas(X,FL,[X|FL]).

%retirar uma Ocorrência da Lista (Ocorrência, Lista, Lista Resultado)
retirarOcorrencia(_,[],[]).
retirarOcorrencia(X,[X|T],L):-retirarOcorrencia(X,T,L).
retirarOcorrencia(X,[H|T],[H|L]):-X\==H,retirarOcorrencia(X,T,L).

%Retira elementos repetidos da Lista (Lista, Lista Resultado)
retirarRepetidos([],[]).
retirarRepetidos([H|T],[H|L1]):-retirarOcorrencia(H,T,L),retirarRepetidos(L,L1).

%Tags Comuns entre 2 Users (User1, User2, Lista Resultado)
tagsComuns(X,Y,L):-tags(X,L1),tags(Y,L2),interseccao(L1,L2,L).


%Funcionalidades
%Amigos dos Amigos (User, Lista Resultado)
listaAmigosAmigos(X,L):-juntar(X,M),retirarOcorrencia(X,M,R)
,retirarRepetidos(R,L).

juntar(X,M):-amigosDosAmigos(X,K),juntaListas(K,M).

amigosDosAmigos(X,[A|L]):- amigosLista(X,A), proximosAmigos(A,L).

proximosAmigos([],_).
proximosAmigos([H|A],[P|L]):- amigosLista(H,P), proximosAmigos(A,L).

%Tamanho da Rede (Nivel 3)
tamanhoRede(X,C):-listaAmigosAmigos(X,L),length(L,C).

%tags em comum entre amigos 
amigosTagsIguais(X,L,P,L1):-length(L,C),amigosLista(X,A),P is 0,outros(X,A,P,C,L1).

tagsComuns(X,Y,L):-tags(X,L1),tags(Y,L2),interseccao(L1,L2,L).

contarElementos(X,[H|A],C,P,[H|L1]):-tagsComuns(X,H,L),length(L,C1)
,C==C1,P1 is P+1,outros(X,A,P1,C,L1).
contarElementos(X,[H|A],C,P,L1):-outros(X,A,P,C,L1).

outros(_,[],_,_,_).
outros(X,A,P,C,L1):-contarElementos(X,A,C,P,L1).

%amigos em comum entre dois utilizadores (User1, User2, Lista de Amigos comuns)
encontrarAmigosComuns(X,Y,L):-amigosLista(X,L1),amigosLista(Y,L2),interseccao(L1,L2,L).

%Sugestão de Coneccões (User, Lista Resultado([User,Lista de Tags em Comum]))
juntarSugestoes(X,S):-sugestoes(X,[2,6,9],L,P),juntarPessoa(L,P,S).

sugestoes(_,[],_,_).
sugestoes(X,[H|T],[H|L],[K|P]):-tagsComuns(X,H,K),length(K,C),C>0,sugestoes(X,T,L,P).
sugestoes(X,[H|T],[H|L],P):-sugestoes(X,T,L,P).

juntarPessoa([],_,_).
juntarPessoa([H|L],P,[H|S]):-juntarTag(L,P,S).
juntarTag(L,[H|P],[H|S]):-juntarPessoa(L,P,S).

%Caminho mais forte (User Origem, User Destino, Percurso + Forte)
maisForte(O,D,P):-maisForte1([(0,0,10,[O])],D,P),reverse(P,P).
maisForte1([(_,_,_,P)|_],D,P):-P=[D|_].
maisForte1([(_,_,_,[D|_])|R],D,L):-!,maisForte1(R,D,L).
maisForte1([(M,C,P,[Ult|T])|O],D,L):-findall((MF,CF,PF,[Z,Ult|T])
,(amigosLista(Ult,T,Z,P1),PF is P+P1,CF is C+1,MF is PF/CF),L1),append(O,L1,L2)
,sort(L2,L3),reverse(L3,L4),maisForte1(L4,D,L).

%Caminho mais curto (User Origem, User Destino, Percuso + curto)
maisCurto(O,D,L):-maisCurto1([[O]],D,P),inverte(P,L).
maisCurto1([L|R],D,L):-L=[D|_].
maisCurto1([[D|T]|R],D,L):-!,maisCurto1(R,D,L).
maisCurto1([[U|T]|RL],D,L):-findall([Z,U|T],amigosLista(U,T,Z),LTMP)
,append(RL,LTMP,LF),maisCurto1(LF,D,L).




%tags em comum entre amigos
amigosTagsIguais(X,L,L1):-amigosLista(X,A),length(L,C),outros(X,A,L,C,L1).

contarelementos(X,[H|A],L,C,[H|L1]):-tagsComuns(X,H,R),interseccao(R,L,M)
,length(M,P),C==P,outros(X,A,L,C,L1).

contarelementos(X,[H|A],L,C,L1):-outros(X,A,L,C,L1).

outros(_,[],_,_,_).
outros(X,A,L,C,L1):-contarelementos(X,A,L,C,L1).






