# psgg-mermaid-flow
Convert StateGo Data File (*.psgg) to Mermaid-js Flow Markdown text.

# What it is.
This tool will convert [Code Mix Visual Programming StateGo](https://statego.programanic.com) data file to [Mermaid](https://mermaid-js.github.io/mermaid/#/) flow chart markdown text.

- StateGo State Chart
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/statego.png)

- Mermaid Flow Chart
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/mermeid.png)

# How to use

## Environment

Windows 10  
Visual Studio 2019  
[StateGo](https://statego.programanic.com/) (If you want to view StateGo Data File)  

## Build 
Open "psgg2mermaid\psgg2mermaid.sln" using Visual Studio 2019.  
Then, build it.  

## Usage

```format
psgg2mermaid PSGG_FILE OUTPUT [-c]
```

PSGG_FILE : StateGo Data File (*.psgg)  
OUTPUT : This tool output text file.   
-c : (Option) Each node contains source code.  

# Test

1. Execute the batch :
<pre>
test.bat
</pre>

This batch will convert from files in "testdata" folder to text files in "testdata_out"

2. Open to [Mermaid Live Editor](https://mermaid-js.github.io/mermaid-live-editor/)

3. Open a file in "testdata_out" folder then copy and paste to the editor.

You will see a flow.

## Result 

### FizzBuzzControl.psgg (php) from https://github.com/NNNIC/psgg-php-sample 
- StateGo 
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/fizzbuzz.png)

- Mermaid output
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/fizzbuzz-m.png)

- Mermaid output with code option
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/fizzbuzz-mc.png)

### MazeControl.psgg (unity c#) from https://github.com/NNNIC/unity-maze-digging
- StateGo 
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/maze.png)

- Mermaid output
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/maze-m.png)

## TestControl.psgg (TyranoScript) from https://github.com/NNNIC/psgg-tyranoscript-sample
- StateGo 
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/test.png)

- Mermaid output
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/test-m1.png)
![](https://raw.githubusercontent.com/NNNIC/psgg-mermaid-flow/master/wiki/test-m2.png)

# Test PHP Version

PHP version has been converted from C# project using Haxe.  

Open the below url with file url.
<pre>
https://statego.programanic.com/mermaid/disp.php?file=FILE_URL  
</pre>

This server can accesss GitHub raw file.

Click the following links to see StateGo Flow with marmaid.

- [psgg-bash-sample/sample/TestControl.psgg](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-bash-sample%2Fmaster%2Fsample%2FTestControl.psgg)
<pre>
https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-bash-sample%2Fmaster%2Fsample%2FTestControl.psgg
</pre>

- [psgg-angular8-sample/master/sample/my-app/src/app/state/src/MainControl.psgg](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-angular8-sample%2Fmaster%2Fsample%2Fmy-app%2Fsrc%2Fapp%2Fstate%2Fsrc%2FMainControl.psgg)
<pre>
https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-angular8-sample%2Fmaster%2Fsample%2Fmy-app%2Fsrc%2Fapp%2Fstate%2Fsrc%2FMainControl.psgg
</pre>


- [psgg-javascript-15puzzle/m1/js/doc/MainControl.psgg
](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-javascript-15puzzle%2Fmaster%2Fm1%2Fjs%2Fdoc%2FMainControl.psgg)
<pre>
https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-javascript-15puzzle%2Fmaster%2Fm1%2Fjs%2Fdoc%2FMainControl.psgg
</pre>

- [psgg-javascript-15puzzle/m1/js/doc/CreatePanelsControl.psgg
](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-javascript-15puzzle%2Fmaster%2Fm1%2Fjs%2Fdoc%2FCreatePanelsControl.psgg)
<pre>
https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-javascript-15puzzle%2Fmaster%2Fm1%2Fjs%2Fdoc%2FCreatePanelsControl.psgg
</pre>

## Special TEST for complicated samples.

- [StateGo View Control](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-mermaid-flow%2Fmaster%2Fwiki%2FViewFormStateControl.psgg)

- [StateGo Idle Control](https://statego.programanic.com/mermaid/disp.php?file=https%3A%2F%2Fraw.githubusercontent.com%2FNNNIC%2Fpsgg-mermaid-flow%2Fmaster%2Fwiki%2Fidle.psgg)


