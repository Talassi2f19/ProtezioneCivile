mergeInto(LibraryManager.library, {

  Mobile: function () {
    return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
  },
  
  SetCookie: function (str) {
    document.cookie = UTF8ToString(str)
  },

  GetCookie: function () {
    var str = document.cookie;
    var bufferSize = lengthBytesUTF8(str) + 1; // calculate the size of null-terminated UTF-8 string
    var buffer = _malloc(bufferSize); // allocate string buffer on the heap
    stringToUTF8(str, buffer, bufferSize); // fill the buffer with the string UTF-8 value
    return buffer; // return the pointer of the allocated string to C#
  },
    
});