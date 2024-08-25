<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace haxe\ds;

use \php\Boot;
use \haxe\IMap;

/**
 * StringMap allows mapping of String keys to arbitrary values.
 * See `Map` for documentation details.
 * @see https://haxe.org/manual/std-Map.html
 */
class StringMap implements IMap {
	/**
	 * @var mixed
	 */
	public $data;

	/**
	 * Creates a new StringMap.
	 * 
	 * @return void
	 */
	public function __construct () {
		#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:35: characters 10-32
		$this1 = [];
		#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:35: characters 3-32
		$this->data = $this1;
	}

	/**
	 * See `Map.remove`
	 * 
	 * @param string $key
	 * 
	 * @return bool
	 */
	public function remove ($key) {
		#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:51: lines 51-56
		if (\array_key_exists($key, $this->data)) {
			#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:52: characters 4-27
			unset($this->data[$key]);
			#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:53: characters 4-15
			return true;
		} else {
			#G:\HaxeToolkit\haxe\std/php/_std/haxe/ds/StringMap.hx:55: characters 4-16
			return false;
		}
	}
}

Boot::registerClass(StringMap::class, 'haxe.ds.StringMap');
