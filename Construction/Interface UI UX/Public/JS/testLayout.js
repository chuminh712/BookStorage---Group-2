/******/ (function (modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if (installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
            /******/
        }
        /******/ 		// Create a new module (and put it into the cache)
        /******/ 		var module = installedModules[moduleId] = {
        /******/ 			i: moduleId,
        /******/ 			l: false,
        /******/ 			exports: {}
                    /******/
        };
        /******/
        /******/ 		// Execute the module function
        /******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
        /******/
        /******/ 		// Flag the module as loaded
        /******/ 		module.l = true;
        /******/
        /******/ 		// Return the exports of the module
        /******/ 		return module.exports;
                /******/
        }
        /******/
        /******/
        /******/ 	// expose the modules object (__webpack_modules__)
        /******/ 	__webpack_require__.m = modules;
        /******/
        /******/ 	// expose the module cache
        /******/ 	__webpack_require__.c = installedModules;
        /******/
        /******/ 	// define getter function for harmony exports
        /******/ 	__webpack_require__.d = function (exports, name, getter) {
        /******/ 		if (!__webpack_require__.o(exports, name)) {
        /******/ 			Object.defineProperty(exports, name, {
        /******/ 				configurable: false,
        /******/ 				enumerable: true,
        /******/ 				get: getter
            /******/
        });
                    /******/
        }
                /******/
        };
        /******/
        /******/ 	// getDefaultExport function for compatibility with non-harmony modules
        /******/ 	__webpack_require__.n = function (module) {
        /******/ 		var getter = module && module.__esModule ?
        /******/ 			function getDefault() { return module['default']; } :
        /******/ 			function getModuleExports() { return module; };
        /******/ 		__webpack_require__.d(getter, 'a', getter);
        /******/ 		return getter;
                /******/
        };
        /******/
        /******/ 	// Object.prototype.hasOwnProperty.call
        /******/ 	__webpack_require__.o = function (object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
        /******/
        /******/ 	// __webpack_public_path__
        /******/ 	__webpack_require__.p = "/";
        /******/
        /******/ 	// Load entry module and return exports
        /******/ 	return __webpack_require__(__webpack_require__.s = 42);
            /******/
        })
        /************************************************************************/
        /******/({
        
        /***/ 42:
        /***/ (function (module, exports, __webpack_require__) {
        
                    module.exports = __webpack_require__(43);
        
        
                    /***/
        }),
        
            /***/ 43:
            /***/ (function (module, __webpack_exports__, __webpack_require__) {
        
                    "use strict";
                    Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
            /* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_debucsser__ = __webpack_require__(44);
                    // only if you installed via NPM
                    // pass all the custom properties you want
        
                    var config = {
                        color: 'red',
                        // color of the outline
                        width: '1px',
                        // width of the outline
                        grayscaleOnDebugAll: false,
                        // apply grayscale filter to every elements
                        customClass: 'checkingCSS' // a class existent in your stylesheet
                        // init the debugger
        
                    };
                    var debug = new __WEBPACK_IMPORTED_MODULE_0_debucsser__["a" /* default */](config).init();
        
                    /***/
        }),
        
            /***/ 44:
            /***/ (function (module, __webpack_exports__, __webpack_require__) {
        
                    "use strict";
                    class Debucsser {
                        constructor(props) {
                            this.config = props || {};
                            this.color = this.config.color || 'palevioletred';
                            this.width = this.config.width || '3px';
                            this.style = this.config.style || 'solid';
                            this.customClass = this.config.customClass || null;
                            this.grayscaleOnDebug = this.config.grayscaleOnDebug || false;
                            this.grayscaleOnDebugAll = this.config.grayscaleOnDebugAll || false;
                            this.string = `${this.width} ${this.style} ${this.color}`;
                            this.mainKey = this.config.mainKey || 17;
                            this.secondKey = this.config.secondKey || 16;
                            this.init = this.init.bind(this);
                            this.debug = this.debug.bind(this);
                            this.debugAll = this.debugAll.bind(this);
                            this.stop = this.stop.bind(this);
                            this.addClass = this.addClass.bind(this);
                            this.labels = this.labels.bind(this);
                            this.createGlobalClass = this.createGlobalClass.bind(this);
                            this.removeGlobalClass = this.removeGlobalClass.bind(this);
                        }
                        init() {
                            // initialize invisible label elements => we'll make it visible on selected keystroke
                            this.label = document.createElement('div');
                            this.label.classList.add('debucsser-label');
                            this.label.style = 'display: none;';
                            document.body.appendChild(this.label);
        
                            this.inject_label_style();
                            this.createDebugStyle();
                            this.debug();
                            this.globalStyle = this.createGlobalClass();
                        }
                        debug() {
                            document.addEventListener('keydown', (key) => {
                                if (key.keyCode == this.mainKey) {
                                    document.addEventListener('mousemove', this.labels, true);
                                    document.addEventListener('mouseover', this.addClass, true);
                                    document.addEventListener('keydown', this.debugAll, true);
                                }
                                this.stop();
                            });
                        }
                        stop() {
                            document.addEventListener('keyup', (key) => {
                                if (key.keyCode == this.mainKey) {
                                    document.removeEventListener('mouseover', this.addClass, true);
                                    document.removeEventListener('mousemove', this.labels, true);
                                    this.label.style = 'display: none;';
                                }
                            })
                        }
                        addClass(over) {
                            over.target.classList.add(this.customClass ? this.customClass : 'debucsser');
                            document.addEventListener('mouseout', (out) => {
                                out.target.classList.remove(this.customClass ? this.customClass : 'debucsser');
                            }, true);
                        }
                        debugAll(key) {
                            if (key.keyCode == this.secondKey) {
                                document.body.appendChild(this.globalStyle);
                                document.addEventListener('keyup', this.removeGlobalClass, true)
                            }
                        }
                        createDebugStyle() {
                            const style = document.createElement('style');
                            style.innerHTML = `
                  .debucsser {
                    outline: ${this.string};
                    ${this.config.grayscaleOnDebug && 'filter: grayscale(100%);'}
                  }
                `;
                            document.body.appendChild(style);
                        }
                        createGlobalClass() {
                            const global = document.createElement('style');
                            global.innerHTML = `
                  * {
                    outline: ${this.string};
                    ${this.config.grayscaleOnDebugAll && 'filter: grayscale(100%);'}
                  }
                `;
                            return global;
                        }
                        removeGlobalClass(key) {
                            if (key.keyCode == this.secondKey) {
                                document.body.removeChild(this.globalStyle);
                            }
                        }
                        labels(e) {
                            if (e.target) {
                                const classList = e.target.classList ? e.target.classList.value.replace('debucsser', '') : undefined;
                                const id = e.target.id ? '#' + e.target.id : undefined;
                                const dimensions = e.target.getBoundingClientRect();
                                this.label.innerHTML = `
                    <h2>class: <strong>${classList || `Â¯\\_(ăƒ„)_/Â¯`}</strong></h2>
                    <h2>id: <strong>${id || `Â¯\\_(ăƒ„)_/Â¯`}</strong></h2>
                    <h2><strong>${dimensions.width.toFixed(0)}px</strong> Ă— <strong>${dimensions.height.toFixed(0)}px</strong></h2>
                  `;
                                this.label.style = `display: block; top:${e.clientY + 20}px; left:${e.clientX + 20}px;`;
                            } else {
                                this.label.style = 'display: none;';
                            }
                        }
                        inject_label_style() {
                            const style = document.createElement('style');
                            style.innerHTML = `
                  .debucsser-label {
                    position: fixed;
                    font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
                    padding: 10px 20px;
                    background: #333;
                    border-radius: 3px;
                    color: #f9f9f9;
                    opacity: 0.9;
                    z-index: 999;
                  }
                  .debucsser-label strong {
                    color: palevioletred;
                  }
                `;
                            document.body.appendChild(style);
                        }
                    }
            /* harmony export (immutable) */ __webpack_exports__["a"] = Debucsser;
        
        
                    /***/
        })
        
            /******/
        });