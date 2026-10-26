import { t as e } from "./main-BRpeuKCG.js";
import { n as t } from "./vendor-ui-Be8JR3ju.js";
import { t as n } from "./password-strength-CR79XtXF.js";
var r = /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
    i = 700,
    a = 30;
function o(...e) {
    let t = {};
    for (let n of e)
        Object.defineProperties(t, Object.getOwnPropertyDescriptors(n));
    return t;
}
var s = () => ({
    _timers: [],
    later(e, t) {
        let n = setTimeout(() => {
            ((this._timers = this._timers.filter((e) => e !== n)), e());
        }, t);
        return (this._timers.push(n), n);
    },
    every(e, t) {
        let n = setInterval(e, t);
        return (this._timers.push(n), n);
    },
    destroy() {
        (this._timers.forEach((e) => {
            (clearTimeout(e), clearInterval(e));
        }),
            (this._timers = []));
    },
}),
    c = () => ({
        showPassword: !1,
        togglePassword() {
            this.showPassword = !this.showPassword;
        },
        get strength() {
            return n(this.form?.password ?? ``);
        },
        segmentClass(e) {
            return e >= Math.ceil(this.strength.score / 1.5) ||
                this.strength.score === 0
                ? ``
                : `bg-${this.strength.color}`;
        },
    });
document.addEventListener(`alpine:init`, () => {
    (t.data(`loginForm`, () =>
        o(s(), c(), {
            form: { email: ``, password: ``, remember: !0 },
            errors: {},
            submitting: !1,
            failed: !1,
            errorMessage: ``,
            validate() {
                let e = {};
                return (
                    this.form.email
                        ? r.test(this.form.email) ||
                        (e.email = `That does not look like an email address.`)
                        : (e.email = `Enter your email address.`),
                    this.form.password || (e.password = `Enter your password.`),
                    (this.errors = e),
                    Object.keys(e).length === 0
                );
            },
            fieldClass(e) {
                return this.errors[e] ? `is-invalid` : ``;
            },
            async submit() {
                this.failed = false;

                if (!this.validate()) {
                    return;
                }

                this.submitting = true;

                try {
                    // Bước 1: Đăng nhập bằng API chung
                    const response = await fetch("/api/Auth/login", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            email: this.form.email.trim(),
                            password: this.form.password, 
                        })
                    });

                    const data = await response.json();

                    if (!response.ok) {

                        sessionStorage.removeItem("accessToken");
                        sessionStorage.removeItem("refreshToken");
                        this.failed = true;
                        this.errors = {};

                        this.errorMessage =
                            response.status === 403
                                ? "Tài khoản không có quyền truy cập trang quản trị."
                                : "Email và mật khẩu đó không đúng. Vui lòng thử lại.";

                        return;
                    }

                    const accessToken = data.token?.accessToken;
                    const refreshToken = data.token?.refreshToken;

                    if (!accessToken || !refreshToken) {
                        throw new Error("API không trả về đầy đủ token.");
                    }

                    // Bước 2: Lưu token
                    sessionStorage.setItem("accessToken", accessToken);
                    sessionStorage.setItem("refreshToken", refreshToken);

                    // Bước 3: Yêu cầu server tạo cookie phiên Admin
                    const adminResponse = await fetch("/api/admin-session/login", {
                        method: "POST",
                        headers: {
                            "Authorization": `Bearer ${accessToken}`
                        },
                        credentials: "same-origin"
                    });


                    if (!adminResponse.ok) {
                        sessionStorage.removeItem("accessToken");
                        sessionStorage.removeItem("refreshToken");

                        this.failed = true;
                        this.errors = {};

                        if (adminResponse.status === 403) {
                            this.errorMessage = "Tài khoản không có quyền truy cập trang quản trị.";
                        } else {
                            this.errorMessage = "Không thể đăng nhập. Vui lòng thử lại sau.";
                            console.error( "Không thể tạo phiên Admin:", adminResponse.status);
                        }
                        return;
                    }

                    // Bước 4: Chỉ chuyển trang khi đã tạo cookie thành công
                    window.location.href = "/AdminWeb/Index";

                } catch (error) {
                    this.failed = true;
                    console.error("Login error:", error);
                } finally {
                    this.submitting = false;
                }
            },
        }),
    ),
        t.data(`registerForm`, () =>
            o(s(), c(), {
                form: { name: ``, email: ``, password: ``, terms: !1 },
                errors: {},
                submitting: !1,
                validate() {
                    let e = {};
                    return (
                        this.form.name.trim() || (e.name = `Enter your full name.`),
                        this.form.email
                            ? r.test(this.form.email) ||
                            (e.email = `That does not look like an email address.`)
                            : (e.email = `Enter your email address.`),
                        this.form.password
                            ? this.form.password.length < 8 &&
                            (e.password = `Use at least 8 characters.`)
                            : (e.password = `Choose a password.`),
                        this.form.terms ||
                        (e.terms = `You need to accept the terms to continue.`),
                        (this.errors = e),
                        Object.keys(e).length === 0
                    );
                },
                fieldClass(e) {
                    return this.errors[e] ? `is-invalid` : ``;
                },
                submit() {
                    this.validate() &&
                        ((this.submitting = !0),
                            this.later(() => {
                                ((this.submitting = !1),
                                    (window.location.href = `./two-factor.html`));
                            }, i));
                },
            }),
        ),
        t.data(`forgotPasswordForm`, () =>
            o(s(), {
                form: { email: `` },
                errors: {},
                submitting: !1,
                sent: !1,
                validate() {
                    let e = {};
                    return (
                        this.form.email
                            ? r.test(this.form.email) ||
                            (e.email = `That does not look like an email address.`)
                            : (e.email = `Enter your email address.`),
                        (this.errors = e),
                        Object.keys(e).length === 0
                    );
                },
                fieldClass(e) {
                    return this.errors[e] ? `is-invalid` : ``;
                },
                submit() {
                    this.validate() &&
                        ((this.submitting = !0),
                            this.later(() => {
                                ((this.submitting = !1), (this.sent = !0));
                            }, i));
                },
            }),
        ),
        t.data(`resetPasswordForm`, () =>
            o(s(), c(), {
                form: { password: ``, confirm: `` },
                errors: {},
                submitting: !1,
                done: !1,
                validate() {
                    let e = {};
                    return (
                        this.form.password
                            ? this.form.password.length < 8 &&
                            (e.password = `Use at least 8 characters.`)
                            : (e.password = `Choose a new password.`),
                        this.form.confirm !== this.form.password &&
                        (e.confirm = `The two passwords do not match.`),
                        (this.errors = e),
                        Object.keys(e).length === 0
                    );
                },
                fieldClass(e) {
                    return this.errors[e] ? `is-invalid` : ``;
                },
                submit() {
                    this.validate() &&
                        ((this.submitting = !0),
                            this.later(() => {
                                ((this.submitting = !1), (this.done = !0));
                            }, i));
                },
            }),
        ),
        t.data(`twoFactorForm`, () =>
            o(s(), {
                digits: [``, ``, ``, ``, ``, ``],
                error: ``,
                submitting: !1,
                cooldown: a,
                init() {
                    this.startCooldown();
                },
                startCooldown() {
                    this.cooldown = a;
                    let e = this.every(() => {
                        (--this.cooldown, this.cooldown <= 0 && clearInterval(e));
                    }, 1e3);
                },
                get code() {
                    return this.digits.join(``);
                },
                get complete() {
                    return this.code.length === 6;
                },
                onInput(e, t) {
                    let n = t.target.value.replace(/\D/g, ``);
                    ((this.digits[e] = n.slice(-1)),
                        (t.target.value = this.digits[e]),
                        (this.error = ``),
                        this.digits[e] && e < 5 && this.focusBox(e + 1));
                },
                onKeydown(e, t) {
                    t.key === `Backspace` &&
                        !this.digits[e] &&
                        e > 0 &&
                        (t.preventDefault(),
                            (this.digits[e - 1] = ``),
                            this.focusBox(e - 1));
                },
                onPaste(e) {
                    e.preventDefault();
                    let t = (e.clipboardData?.getData(`text`) ?? ``)
                        .replace(/\D/g, ``)
                        .slice(0, 6);
                    if (t) {
                        for (let e = 0; e < 6; e += 1) this.digits[e] = t[e] ?? ``;
                        this.focusBox(Math.min(t.length, 5));
                    }
                },
                focusBox(e) {
                    let t = this.$refs[`digit${e}`];
                    t && (t.focus(), t.select());
                },
                resend() {
                    this.cooldown > 0 ||
                        ((this.digits = [``, ``, ``, ``, ``, ``]),
                            (this.error = ``),
                            this.startCooldown(),
                            this.focusBox(0));
                },
                submit() {
                    if (!this.complete) {
                        this.error = `Enter all six digits.`;
                        return;
                    }
                    ((this.submitting = !0),
                        this.later(() => {
                            ((this.submitting = !1), (window.location.href = "/AdminWeb/Index"));
                        }, i));
                },
            }),
        ),
        t.data(`lockScreenForm`, () =>
            o(s(), c(), {
                form: { password: `` },
                error: ``,
                submitting: !1,
                submit() {
                    if (!this.form.password) {
                        this.error = `Enter your password to unlock.`;
                        return;
                    }
                    ((this.error = ``),
                        (this.submitting = !0),
                        this.later(() => {
                            ((this.submitting = !1), (window.location.href = "/AdminWeb/Index"));
                        }, i));
                },
            }),
        ));
});
function l() {
    (new e(),
        document.querySelectorAll(`[data-theme-toggle]`).forEach((e) => {
            e.addEventListener(`click`, () => {
                let e =
                    document.documentElement.getAttribute(`data-bs-theme`) === `dark`
                        ? `light`
                        : `dark`;
                document.documentElement.setAttribute(`data-bs-theme`, e);
                try {
                    localStorage.setItem(`theme`, e);
                } catch { }
            });
        }),
        document.addEventListener(`click`, (e) => {
            if (e.target.closest(`[data-go-back]`)) {
                (e.preventDefault(),
                    window.history.length > 1
                        ? window.history.back()
                    : (window.location.href = "/AdminWeb/Index"));
                return;
            }
            e.target.closest(`[data-reload]`) &&
                (e.preventDefault(), window.location.reload());
        }),
        t.start());
}
document.readyState === `loading`
    ? document.addEventListener(`DOMContentLoaded`, l, { once: !0 })
    : l();
