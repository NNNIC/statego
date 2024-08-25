<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace system;

use \php\Boot;

class CsRef {
	/**
	 * @var mixed
	 */
	public $Value;

	/**
	 * @param mixed $v
	 * 
	 * @return void
	 */
	public function __construct ($v) {
		#src/system/CsRef.hx:10: characters 3-17
		$this->Value = $v;
	}

	/**
	 * @return string
	 */
	public function toString () {
		#src/system/CsRef.hx:15: characters 3-32
		return \Std::string($this->Value);
	}

	public function __toString() {
		return $this->toString();
	}
}

Boot::registerClass(CsRef::class, 'system.CsRef');
