package system;

class SystemException
{
    public function new(s:String)
    {
        trace(s);
        throw s;
    }
    public var Message : String;

}