using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifaGame.webservice
{
    public class def_photo
    {
        public string comp_id;


        public string  imageString="/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAKBueIx4ZKCMgoy0qqC+8P//8Nzc8P//////////////\n" +
            "////////////////////////////////////////////2wBDAaq0tPDS8P//////////////////\n" +
            "////////////////////////////////////////////////////////////wAARCADIAMgDASIA\n" +
            "AhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQA\n" +
            "AAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3\n" +
            "ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWm\n" +
            "p6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEA\n" +
            "AwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSEx\n" +
            "BhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElK\n" +
            "U1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3\n" +
            "uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwCaiiig\n" +
            "AooooAKKKKACigkAZJxUTTf3R+NAEtNLqOpqBmZuppKYiYzL2BNJ53+zUVFAEnnH0FHnH+7UdFAE\n" +
            "wmHcGnCRT3x9ar0UAWqKqgkdDipFmP8AEM0hk1FIrBhwaWgAooooAKKKKACiiigAooooAKKKKACm\n" +
            "PIF4HJpskvZfzqKgBWYsck0lFFMQUUUUAFFFFABRRRQAUUUUAFFFFAACQcipUl7N+dRUUAWqKgjk\n" +
            "K8HkVODkZFIYUUUUAFFFFABRRRQAVFLJ/CPxp0r7RgdTUFABRRRTEFFFFABRRRQAUUU4Rse2PrQA\n" +
            "2inmJvY0wqR1BFABRRRQAUUUUAFFFFABT432nB6UyigC1RUML/wn8KmpDCiiigAoJwCTRUUzdF/O\n" +
            "gCNiWJJpKKKYgooooAKKKKACgc0VLCvVqAHRxheTyafRRSGFFFFADGiU+xqFlKnBqzSOu5cUAVqK\n" +
            "KKYgooooAKKKKACrEbbl9+9V6fE21/Y0AT0UUUhhVZjuYmp5DhDVemIKKKKACiiigAooooAKnh+5\n" +
            "UFTQ/cP1oAkooopDCiiigAooooArP98/WkpWOWJ96SmIKKKKACiiigAooooAsodyg0VHAeCKKQxZ\n" +
            "zwBUNST/AHh9KjpiCiiigAooooAKKKKACp4gAmfWoKmhb5cdxQBJRRRSGFFFFABRRQSFBJoAqsMM\n" +
            "R70UHk0UxBRRRQAUUUUAFFFFAD4Th/rRSR/fFFIY6f74+n+NR1JP94fSo6YgooooAKKKKACiiigA\n" +
            "p0Rw49+KbRQBaopqNuXPfvTqQwooooAKjmPygetSHgZNVnbc2aAEooopiCiiigAooooAKKKKAFT7\n" +
            "6/UUUJ98fWikMknHQ1FU8wyn0qCmIKKKKACiiigAooooAKKKesbN14FADUYqcirCsGGRSbF27ccV\n" +
            "GY2U5Xn6Uhk1BOBk1B5knT+lG1365/GgAkk3cDp/OmVOsagYPNMeIjleaYiOigjHWigAooooAKKK\n" +
            "KACiiigB0QzIKKdAOSaKQyYjII9aqng4q1UEy4bPrQAyiiimIKKKKAAAk4FSrD/eP5U6Jdq57mn0\n" +
            "hiKir0FLRRQAUUUUAFFFFABRRRQAEA9Rmo2hB+6cVJRQBWZSp5FJVllDDBqsRg4NMQUUUUAFFFKi\n" +
            "7mAoAniGEHvzRTqKQwprruUinUUAVaKkmTB3D8ajpiClQbmApKkgHzE+lAE1FFFIYUUUUAFFFFAB\n" +
            "RRRQAUUUUAFFFFABUMww2fWpqjmGUz6GgCGiiimIKmhXA3HvUcabm9u9WKACiiikMKKKKAAjIwar\n" +
            "umw+1WKCAwwaAKtTQD5Sfeo3Qofb1qWHGymIfRRRSGFFFFABRRRQAUUUUAFFFFABRRRQAU2QZQ/S\n" +
            "nUjY2nNAFahQWOBQoLHAqwiBB70xCqoUYFLRRSGFFFFABRRRQAUUUUABAIwahZGQ7l6VNRQAxJQe\n" +
            "DwafTHjDcjg0z54/cfpQBNRUazKevFSAg9DmgAooooAKKKKACiijpQAUUxpVHfP0pm934UYH+e9A\n" +
            "EjSBfc+lR/NKfQU5YgOW5qSgBFUKMCloooAKKKKACiiigAooooAKKKKACiiigAooooAa0at2/KmG\n" +
            "Ej7rUUUAJiVff9aN8g6r+lFFAB5r/wB39DR5kh/h/Q0UUxB+9Pt+lHlMfvN/WiikMesSj3+tP6UU\n" +
            "UAFFFFABRRRQAUUUUAFFFFAH/9k=\n";
    }
}