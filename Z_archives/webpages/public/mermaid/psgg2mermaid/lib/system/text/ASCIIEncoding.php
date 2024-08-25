<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace system\text;

use \php\Boot;
use \haxe\Exception;
use \system\NotImplementedException;
use \haxe\io\Bytes;

class ASCIIEncoding extends Encoding {
	/**
	 * @return void
	 */
	public function __construct () {
		#src/system/text/ASCIIEncoding.hx:10: characters 3-10
		parent::__construct();
	}

	/**
	 * @param string $str
	 * 
	 * @return Bytes
	 */
	public function GetBytes_String ($str) {
		#src/system/text/ASCIIEncoding.hx:15: characters 10-15
		throw Exception::thrown(new NotImplementedException());
	}
}

Boot::registerClass(ASCIIEncoding::class, 'system.text.ASCIIEncoding');
