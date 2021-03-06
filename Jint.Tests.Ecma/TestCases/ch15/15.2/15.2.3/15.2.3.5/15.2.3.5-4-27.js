/// Copyright (c) 2012 Ecma International.  All rights reserved. 
/**
 * @path ch15/15.2/15.2.3/15.2.3.5/15.2.3.5-4-27.js
 * @description Object.create - own enumerable accessor property in 'Properties' without a get function that overrides an enumerable inherited accessor property in 'Properties' is defined in 'obj' (15.2.3.7 step 5.a)
 */


function testcase() {

        var proto = {};
        Object.defineProperty(proto, "prop", {
            get: function () {
                return {};
            },
            enumerable: true
        });

        var ConstructFun = function () { };
        ConstructFun.prototype = proto;

        var child = new ConstructFun();
        Object.defineProperty(child, "prop", {
            set: function () { },
            enumerable: true
        });

        try {
            Object.create({}, child);

            return false;
        } catch (ex) {
            return ex instanceof TypeError;
        }
    }
runTestCase(testcase);
