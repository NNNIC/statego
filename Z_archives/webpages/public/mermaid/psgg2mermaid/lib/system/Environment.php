<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace system;

use \php\Boot;
use \haxe\Exception as HaxeException;

class Environment {
	/**
	 * @var string
	 */
	const NewLine = "\x0A";


	/**
	 * @return mixed
	 */
	public static function get_OSVersion () {
		#src/system/Environment.hx:31: characters 10-15
		throw HaxeException::thrown(new NotImplementedException());
	}

	/**
	 * @return int
	 */
	public static function get_TickCount () {
		#src/system/Environment.hx:24: characters 10-39
		return (int)((\microtime(true) * 1000));
	}

	/**
	 * @return void
	 */
	public function __construct () {
	}
}

Boot::registerClass(Environment::class, 'system.Environment');
Boot::registerGetters('system\\Environment', [
	'OSVersion' => true,
	'TickCount' => true
]);
