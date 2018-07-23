<Query Kind="Program" />

/*
This problem was asked by Amazon.

Run-length encoding is a fast and simple method of encoding strings. 
The basic idea is to represent repeated successive characters as a single count and character. 
For example, the string "AAAABBBCCDAA" would be encoded as "4A3B2C1D2A".

Implement run-length encoding and decoding. 
You can assume the string to be encoded have no digits and consists solely of alphabetic characters. 
You can assume the string to be decoded is valid.
*/
void Main()
{
	encode("AAAABBBCCDAA").Dump("Expect: 4A3B2C1D2A");
	decode("4A3B2C1D2A").Dump("Expect: AAAABBBCCDAA");
	encode("AAAAAAAAAAAB").Dump("Expect: 11A1B");
	decode("11A1B").Dump("Expect: AAAAAAAAAAAB");
}

//case sensitive
string encode(string msg){
	StringBuilder sb = new StringBuilder();
	char prev = msg[0];
	int count=1;
	for (int i = 1; i<msg.Length; i++){
		if (msg[i]==prev){
			count++;
		}
		else{
			sb.Append(count);
			sb.Append(prev);
			prev=msg[i];
			count=1;
		}
	}
	//final character
	sb.Append(count);
	sb.Append(prev);
	return sb.ToString();
}

string decode(string msg)
{
	StringBuilder sb = new StringBuilder();
	int count = int.Parse(msg[0].ToString());
	for (int i = 1; i < msg.Length; i++)
	{
		if (char.IsLetter(msg[i]) ){
			while(count-->0){
				sb.Append(msg[i]);
			}
			count=0;
		}
		else{
			count*=10;	//shift
			count+=int.Parse(msg[i].ToString()); //add
		}
		
	}
	//in a valid string there would be no remaining character
	
	return sb.ToString();


}