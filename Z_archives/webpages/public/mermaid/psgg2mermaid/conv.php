<?php
use \start\Program;
use \haxe\EntryPoint;

set_include_path(get_include_path().PATH_SEPARATOR.__DIR__.'/lib');
spl_autoload_register(
	function($class){
		$file = stream_resolve_include_path(str_replace('\\', '/', $class) .'.php');
		if ($file) {
			include_once $file;
		}
	}
);
\php\Boot::__hx__init();

$file = $_GET['file'];
//$file = 'https://raw.githubusercontent.com/NNNIC/psgg-ruby-sample/master/sample/TestControl.psgg';

// --------------- read data ref : https://stackoverflow.com/questions/23873630/how-to-get-github-raw-file-contents-with-php
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, $file);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
$data = curl_exec($ch);
curl_close($ch);

//echo '# pass get file';

$text = Program::conv($data,false);

//echo '# pass convert';
echo $text;

//$mermaid = Psgg2mermaidProgram::Convert($data,false);

//echo $mermaid;


//$data = file_get_contents('https://raw.githubusercontent.com/NNNIC/psgg-ruby-sample/master/sample/TestControl.psgg'); 
//echo $data
//Program::main();
//EntryPoint::run();
