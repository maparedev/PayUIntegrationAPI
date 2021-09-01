﻿
var gulp = require("gulp");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");

function minify() {

    return gulp.src(["wwwroot/client/**/*.js"])
        .pipe(uglify())
        .pipe(concat("jkcshopping.min.js"))
        .pipe(gulp.dest("wwwroot/dist/"));
}


function styles() {

    return gulp.src(["wwwroot/css/*.css"])
        .pipe(uglify())
        .pipe(concat("jkcshopping.min.css"))
        .pipe(gulp.dest("wwwroot/dist/"));
}


exports.minify = minify;
exports.styles = styles;

exports.default = gulp.parallel(minify, styles);
