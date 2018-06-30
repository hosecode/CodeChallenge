<Query Kind="Program" />

/*
Good morning! Here's your coding interview problem for today. #5
This problem was asked by Jane Street.
cons(a, b) constructs a pair, and car(pair) and cdr(pair) returns the first and last element of that pair. For example, car(cons(3, 4)) returns 3, and cdr(cons(3, 4)) returns 4.
Given this implementation of cons:
def cons(a, b):
    def pair(f):
        return f(a, b)
    return pair
Implement car and cdr.
*/

void Main(){
	var c = cons<int>(5, 6).Dump("cons<int>");
	car<int>(c).Dump("car<int> Expect: 5"); //args and return are ints
	cdr<int>(c).Dump("cdr<int> Expect: 6"); //args and return are ints
	//car<char>(c).Dump("car<char>");	//Wont Compile: Cannot conver from a func that takes ints to one that takes char
	concatPair(c).Dump("carString Expect: '(5,6)'"); //even though args are ints, return type is string
	concatPair<int>(c).Dump("carString Expect: '(5,6)'"); //same as above, just explicitly declared <int>

	var c2 = cons<char>('a', 'b').Dump("cons<char>"); //now lets set input type as char
	car<char>(c2).Dump("car<char> Expect: 'a'"); //now we want first char
	cdr<char>(c2).Dump("cdr<char> Expect: 'b'"); //second char
	concatPair(c2).Dump("carString Expect: '(a,b)'"); // this is still a string
	concatPair<char>(c2).Dump("carString<char> Expect: '(a,b)'"); //same as above explicit declare
}

//Returns a function that takes a func as param. That func must return obj and take (T, T). 
//When returned func is called, a and b will be past to arg func as the arguments. 
//func returns objects, because we don't know what our callers will pass in, or what they want
//to return 
Func<Func<T, T, object>, object> cons<T>(T a, T b) {
	return (f) => { return f(a,b); }; 
}
//takes a 'pair' func, returns first element, so return type is same as arg type T
T car<T>(Func<Func<T, T, object>, object> f) {
	return (T)f((a, b) => {return a; });
}
T cdr<T>(Func<Func<T, T, object>, object> f){
	return (T)f((a, b) => { return b; });
}
//return a concat string of a pair of T
string concatPair<T>(Func<Func<T, T, object>, object> f)
{
	//cast here because func parameter is object return type.
	//but since we own the logic of what is returned, we know we return a string
	return (string)f((a, b) => { return $"({a},{b})"; });
}