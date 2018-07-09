<Query Kind="Program" />

/*
This problem was asked by Google.

Suppose we represent our file system by a string in the following manner:

The string "dir\n\tsubdir1\n\tsubdir2\n\t\tfile.ext" represents:

dir
    subdir1
    subdir2
        file.ext
The directory dir contains an empty sub-directory subdir1 and a sub-directory subdir2 containing a file file.ext.

The string "dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext" represents:

dir
    subdir1
        file1.ext
        subsubdir1
    subdir2
        subsubdir2
            file2.ext
The directory dir contains two sub-directories subdir1 and subdir2. subdir1 contains a file file1.ext and an empty second-level sub-directory subsubdir1. 
subdir2 contains a second-level sub-directory subsubdir2 containing a file file2.ext.

We are interested in finding the longest (number of characters) absolute path to a file within our file system. 
For example, in the second example above, the longest absolute path is "dir/subdir2/subsubdir2/file2.ext", and its length is 32 (not including the double quotes).

Given a string representing the file system in the above format, return the length of the longest absolute path to a file in the abstracted file system. 
If there is no file in the system, return 0.

Note:
The name of a file contains at least a period and an extension.
The name of a directory or sub-directory will not contain a period.
*/
void Main()
{
	Solve("dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext").Dump("Expect: 32");
	Solve("dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext\naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.txt").Dump("Expect: 62");
	Solve("file2.ext").Dump("Expect: 9");
	Solve("").Dump("Expect: 0");
}


int Solve(string s){
	StringReader sr = new StringReader(s);
	string token=null;
	Stack<string> dirs = new Stack<string>();
	int depth=0;
	int curCount=0;
	int max=0;
	while (null != (token = sr.ReadLine())) {
		depth = token.LastIndexOf('\t')+1;
		if (token.Contains('.')){
			if (max<curCount+token.Replace("\t","").Length) {
				max = curCount + token.Replace("\t", "").Length;
				//token.Dump($"Max {max}");
			}
		}
		else {
			while (depth  < dirs.Count)
			{
				curCount -= dirs.Pop().Length + 1;
			}
			curCount += token.Replace("\t", "").Length + 1;
			dirs.Push(token.Replace("\t", ""));
		}
	}
	return max;
}

string Tokenize(StringReader sr){
	return sr.ReadLine();	
}