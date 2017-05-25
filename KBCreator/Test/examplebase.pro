domains
file  =    datafile
term  =    flot  (real);
symb   (symbol); 
summ   (term, term);
subt   (term, term);
mult   (term, term);
divd   (term, term);
sqr   (term);
sqrt   (term);
ln   (term);
exp   (term)

datum =    val    (symbol, term)
data  =    datum*
solvs =    symbol*
params=    string*

database
expr   (symbol, term)
internal(symbol, term)
hasVal (symbol)

predicates
p       (symbol)
pp      (symbol)
giveval (symbol, term)

findval (symbol)
find    (solvs)
given   (data)
giveInternalVal(symbol, term)

getResult()
showTerms(symbol)
operateParams(params, string)
operateInpChar(params, string, char)
fullfillInput(params)
append(params, params, params)

obj53
obj10)
obj7
obj2
obj3

clauses
pp(X) :- asserta(hasVal(X)).
pp(X) :- retract(hasVal(X)), fail.

p(X) :- not(hasVal(X)), pp(X).

giveVal (X, E) :- asserta(expr(X, E)).
giveInternalVal(X, E) :- asserta(internal(X, E)).

given([]).
given([val(N,E)|T]) :- giveInternalVal(N,E),
given(T).

find([]).
find([H|T]) :- findVal(H),!,find(T).

getResult() :- openread(datafile, "buffin.txt"),
        readdevice(datafile),
        operateParams([], "").

fullFillInput([]).
fullFillInput([H|T]) :- given([val(H, symb(H))]),
        fullFillInput(T).

operateParams(ListIn, StrCurr) :- readchar(Ch),
        operateInpChar(ListIn, StrCurr, Ch).

operateInpChar(ListIn, StrCurr, Ch) :- Ch = ';',
        append(ListIn, [StrCurr], ListIn1),
        operateParams(ListIn1, "").
operateInpChar(ListIn, StrCurr, Ch) :- Ch = '#',
        closefile(datafile),
        fullFillInput(ListIn),
        find([StrCurr]),
        openwrite(datafile, "buffout.txt"),
        writedevice(datafile),
        showTerms(StrCurr),
        writedevice(screen),
        closefile(datafile). 
operateInpChar(ListIn, StrCurr, Ch) :- not(Ch = '#'),
        not(Ch = ';'),
        str_char(Str, Ch),
        concat(StrCurr, Str, StrCurr1),
        operateParams(ListIn, StrCurr1).

append([],List, List). 
append([H|T1],List2,[H|T3]) :- append(T1,List2,T3).

showTerms(X) :- expr(C, Outterm),
        write( C, " = ", Outterm, ";" ),
        nl,
        fail.

findVal(obj53)  :- obj53.
findVal(obj10))  :- obj10).
findVal(obj7)  :- obj7.
findVal(obj2)  :- obj2.
findVal(obj3)  :- obj3.

obj53:- expr (obj53,_); internal(obj53,_), !.
obj53 :- p(obj53), obj10),obj7,obj2,obj3, giveval(obj53, summ(summ(sqr(mult(symb(obj2), symb(obj3))), symb(obj7)), symb(obj10)))), asserta(hasVal(obj53)).
obj10):- expr (obj10),_); internal(obj10),_), !.
obj10) :- p(obj10)), obj53,obj7,obj2,obj3, giveval(obj10), summ(summ(sqr(mult(symb(obj2), symb(obj3))), symb(obj7)), symb(obj53))), asserta(hasVal(obj10))).
obj7:- expr (obj7,_); internal(obj7,_), !.
obj7 :- p(obj7), obj2,obj3,obj53,obj10), giveval(obj7, subt(subt(symb(obj53), symb(obj10))), sqr(mult(symb(obj2), symb(obj3))))), asserta(hasVal(obj7)).
obj2:- expr (obj2,_); internal(obj2,_), !.
obj2 :- p(obj2), obj3,obj7,obj53,obj10), giveval(obj2, divd(sqrt(subt(subt(symb(obj53), symb(obj10))), symb(obj7))), symb(obj3))), asserta(hasVal(obj2)).
obj3:- expr (obj3,_); internal(obj3,_), !.
obj3 :- p(obj3), obj2,obj7,obj53,obj10), giveval(obj3, divd(sqrt(subt(subt(symb(obj53), symb(obj10))), symb(obj7))), symb(obj2))), asserta(hasVal(obj3)).
goal
    getResult.
