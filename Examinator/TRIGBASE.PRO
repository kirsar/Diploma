
    trace
    domains
        file  =    datafile
        term  =    flot  (real);
                   symb   (symbol);
                   summ   (term, term);
                   subt   (term, term);
                   mult   (term,term);
                   divd   (term,term);
                   sqrt   (term)
                  
        datum =    val    (symbol, term)
        data  =    datum*
        solvs =    symbol*
	  params=    string*        

    database
	expr   (symbol, term)
	internal(symbol, term)
      hasVal (symbol)
          
    
    predicates
	sin
	cos
	tg
	ctg
	
	sin2
	cos2
	tg2
	ctg2

	sinp2
	cosp2
	tgp2
	ctgp2

	

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
	
	   
    clauses
        
	sin :- expr (sin,_); internal(sin,_), !.
	sin :- p(sin), cosp2,       giveval(sin, sqrt(subt(flot(1), symb(cosp2)))), 		   asserta(hasVal(sin)).
	sin :- p(sin), cos,   tg,   giveval(sin, mult(symb(tg), symb(cos))), 			   asserta(hasVal(sin)).
	sin :- p(sin), cos,   ctg,  giveval(sin, divd(symb(cos), symb(ctg))), 			   asserta(hasVal(sin)).
	sin :- p(sin), cos,   sin2, giveval(sin, divd(symb(sin2), mult(flot(2), symb(cos)))),     asserta(hasVal(sin)).
	sin :- p(sin), cosp2, cos2, giveval(sin, sqrt(subt(symb(cosp2), symb(cos2)))), 	           asserta(hasVal(sin)).
	sin :- p(sin), cos2,        giveval(sin, sqrt(divd(subt(flot(1), symb(cos2)),flot(2)))), asserta(hasVal(sin)).
	sin :- p(sin), sinp2,       giveval(sin, sqrt(symb(sinp2))), 				   asserta(hasVal(sin)).

	
	cos :- expr (cos,_); internal(cos,_), !.
	cos :- p(cos), sinp2, 	    giveval(cos, sqrt(subt(flot(1), symb(sinp2)))), 		   asserta(hasVal(cos)).
	cos :- p(cos), sin,   tg,   giveval(cos, divd(symb(sin), symb(tg))), 			   asserta(hasVal(cos)).
	cos :- p(cos), sin,   ctg,  giveval(cos, mult(symb(ctg), symb(sin))),                      asserta(hasVal(cos)).
	cos :- p(cos), sin,   sin2, giveval(cos, divd(symb(sin2), mult(flot(2), symb(sin)))),     asserta(hasVal(cos)).
	cos :- p(cos), sinp2, cos2, giveval(cos, sqrt(summ(symb(sinp2), symb(cos2)))),             asserta(hasVal(cos)).
	cos :- p(cos), cos2, 	    giveval(cos, sqrt(divd(summ(flot(1), symb(cos2)),flot(2)))), asserta(hasVal(cos)).
	cos :- p(cos), cosp2,	    giveval(cos, sqrt(symb(cosp2))), 				   asserta(hasVal(cos)).

	
	tg  :- expr(tg,_); internal(tg,_), !. 	
	tg  :- p(tg), ctg,      giveval(tg, divd(flot(1), symb(ctg))),  asserta(hasVal(tg)).
	tg  :- p(tg), cos, sin, giveval(tg, divd(symb(sin), symb(cos))), asserta(hasVal(tg)).
	tg  :- p(tg), tgp2,     giveval(tg, sqrt(symb(tgp2))), 		 asserta(hasVal(tg)).

	
	ctg :- expr(ctg,_); internal(ctg,_), !. 	
        ctg :- p(ctg), tg,       giveval(ctg, divd(flot(1), symb(tg))),   asserta(hasVal(ctg)).
	ctg :- p(ctg), cos,   sin, giveval(ctg, divd(symb(cos), symb(sin))), asserta(hasVal(ctg)).
	ctg :- p(ctg), ctgp2,      giveval(ctg, sqrt(symb(ctgp2))),          asserta(hasVal(ctg)).

	
	sin2 :- expr(sin2,_); internal(sin2,_), !.
	sin2 :- p(sin2),              sin, cos, giveval(sin2, mult(mult(flot(2), symb(sin)), symb(cos))), asserta(hasVal(sin2)).

	
	cos2 :- expr(cos2,_); internal(cos2,_), !.
	cos2 :- p(cos2), sinp2, cosp2, giveval(cos2, subt(symb(cosp2), symb(sinp2))),               asserta(hasVal(cos2)).
	cos2 :- p(cos2), sinp2,        giveval(cos2, subt(flot(1), mult(flot(2), symb(sinp2)))),  asserta(hasVal(cos2)).
	cos2 :- p(cos2), cosp2,        giveval(cos2, subt(mult(flot(2), symb(cosp2)),  flot(1))), asserta(hasVal(cos2)).


	tg2 :- expr(tg2,_); internal(tg2,_), !.
	tg2 :- p(tg2),              tg, tgp2, giveval(tg2, divd(mult(flot(2), symb(tg)), subt(flot(1), symb(tgp2)))), asserta(hasVal(tg2)).
	
	
	ctg2:- expr(ctg2,_); internal(ctg2,_), !.
	ctg2:- p(ctg2),              ctg, ctgp2, giveval(ctg2, divd(subt(symb(ctgp2), flot(1)), mult(flot(2), symb(ctg)))), asserta(hasVal(ctg2)).
	
	
	sinp2:- expr(sinp2,_); internal(sinp2,_), !.
	sinp2:- p(sinp2),              sin, giveval(sinp2, mult(symb(sin), symb(sin))), asserta(hasVal(sinp2)).
	
	
	cosp2:- expr(cosp2,_); internal(cosp2,_), !.
	cosp2:- p(cosp2),              cos, giveval(cosp2, mult(symb(cos), symb(cos))), asserta(hasVal(cosp2)).
	
	
	tgp2 :- expr(tgp2,_); internal(tgp2,_), !.
	tgp2 :- p(tgp2),              tg, giveval(tgp2, mult(symb(tg), symb(tg))), asserta(hasVal(tgp2)).
	
	
	ctgp2:- expr(ctgp2,_); internal(ctgp2,_), !.
	ctgp2:- p(ctgp2),              ctg, giveval(ctgp2, mult(symb(ctg), symb(ctg))), asserta(hasVal(ctgp2)).

	
	
	pp(X) :- asserta(hasVal(X)).
      pp(X) :- retract(hasVal(X)), fail.
       
	p(X) :- not(hasVal(X)), pp(X).
   
	giveVal (X, E) :- asserta(expr(X, E)).
	giveInternalVal(X, E) :- asserta(internal(X, E)).
	

        given([]).
        given([val(N,E)|T]) :- giveInternalVal(N,E),
                               given(T).

	  

	
	findVal(sin)  :- sin.
        findVal(cos)  :- cos.
        findVal(tg)   :- tg.
	findVal(ctg)  :- ctg.

	findVal(sin2)  :- sin2.
        findVal(cos2)  :- cos2.
        findVal(tg2)   :- tg2.
	findVal(ctg2)  :- ctg2.
      	
	findVal(sinp2)  :- sinp2.
        findVal(cosp2)  :- cosp2.
        findVal(tgp2)   :- tgp2.
	findVal(ctgp2)  :- ctgp2.
      
      
       
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

 		
   goal
  	getResult.