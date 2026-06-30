import { useState } from 'react'
import { login } from "../services/authService";
import type { LoginRequest } from "../types/auth";
import { Link } from 'react-router-dom'
import { showToast } from "../utils/toast";
import authImage from "../assets/AuthImage.jpg";
interface FieldProps {
    label: string
    type?: string
    placeholder: string
    value: string
    onChange: (v: string) => void
    error?: string
}

function Field({
                   label,
                   type = "text",
                   placeholder,
                   value,
                   onChange,
                   error,
               }: FieldProps) {
    const [showPassword, setShowPassword] = useState(false);

    const inputType =
        type === "password"
            ? (showPassword ? "text" : "password")
            : type;

    return (
        <div className="flex flex-col gap-1.5">
            <label className="text-[10px] font-semibold tracking-[0.8px] uppercase text-[#7a99bb]">
                {label}
            </label>

            <div className="relative">
                <input
                    type={inputType}
                    placeholder={placeholder}
                    value={value}
                    onChange={e => onChange(e.target.value)}
                    autoComplete="off"
                    className={`w-full px-3.5 pr-16 py-[11px] bg-[#0a1525] border rounded-[7px] text-sm text-[#e8f0fe] placeholder-[#4a6380] font-[inherit] outline-none transition-all duration-200
                    focus:border-[#00e5c0] focus:shadow-[0_0_0_3px_rgba(0,229,192,0.1)]
                    ${error ? "border-[#e05c5c]" : "border-[#1e3254]"}`}
                />

                {type === "password" && (
                    <button
                        type="button"
                        onClick={() => setShowPassword(!showPassword)}
                        className="absolute right-3 top-1/2 -translate-y-1/2 text-xs text-[#7a99bb] hover:text-[#00e5c0]"
                    >
                        {showPassword ? "Hide" : "Show"}
                    </button>
                )}
            </div>

            {error && (
                <span className="text-[11px] text-[#e05c5c]">
                    {error}
                </span>
            )}
        </div>
    );
}

export default function LoginPage() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [errors, setErrors] = useState<Record<string, string>>({})

    const validate = (): boolean => {
        const e: Record<string, string> = {}
        if (!email.includes('@')) e.email = 'Enter a valid email address'
        if (!password) e.password = 'Password is required'
        setErrors(e)
        return Object.keys(e).length === 0
    }

    const handleSignIn = async () => {
        if (!validate()) return;

        try {
            const response = await login({
                email,
                password,
            });

            showToast.success("Login successful");
        } catch (err: any) {
            showToast.error(err.message || "Login failed");
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-[#0b1628] font-[Inter,system-ui,sans-serif]"
             style={{
                 backgroundImage: 'linear-gradient(rgba(30,50,84,0.35) 1px, transparent 1px), linear-gradient(90deg, rgba(30,50,84,0.35) 1px, transparent 1px)',
                 backgroundSize: '40px 40px',
             }}
        >
            <div className="flex w-[780px] min-h-[520px] bg-[#0f1e35] rounded-2xl border border-[#1e3254] overflow-hidden shadow-[0_24px_80px_rgba(0,0,0,0.6)]">

                {/* ── Left decorative panel ── */}
                <div className="relative w-[45%] flex-shrink-0 bg-[#0d1a2e] border-r border-[#1e3254] flex items-end p-8 overflow-hidden">
                    {/* Glow */}
                    <div className="absolute top-[60px] left-1/2 -translate-x-1/2 w-[180px] h-[180px] rounded-full pointer-events-none"
                         style={{ background: 'radial-gradient(circle, rgba(0,229,192,0.18) 0%, transparent 70%)' }} />
                    <img
                        src={authImage}
                        alt="DevSphere"
                        className="absolute inset-0 w-full h-full object-cover opacity-30"
                    />
                    
                    
                    {/* Brand */}
                    <div className="relative z-10">
                        <Link to="/" className="no-underline">
                            <span className="text-[22px] font-bold text-[#e8f0fe] tracking-[-0.3px]">Dev</span>
                            <span className="text-[22px] font-bold text-[#00e5c0] tracking-[-0.3px]">Sphere</span>
                        </Link>
                        <p className="text-[11px] text-[#4a6380] mt-1 tracking-[0.3px]">
                            Unified · Secure · Shared Infrastructure
                        </p>
                    </div>
                </div>

                {/* ── Right form panel ── */}
                <div className="relative flex-1 flex flex-col px-9 py-8">
                    {/* Status dot */}
                    <div className="absolute top-[18px] right-5 w-2.5 h-2.5 rounded-full bg-[#00e5c0] shadow-[0_0_8px_#00e5c0]" />

                    {/* Tabs */}
                    <div className="flex border border-[#1e3254] rounded-lg overflow-hidden mb-7">
                        <button className="flex-1 py-2.5 bg-[#0f3d30] text-[#00e5c0] text-sm font-semibold cursor-pointer border-none font-[inherit]">
                            Sign In
                        </button>
                        <Link to="/register" className="flex-1 flex items-center justify-center py-2.5 text-[#7a99bb] text-sm font-medium no-underline hover:text-[#e8f0fe] transition-colors">
                            Register
                        </Link>
                    </div>

                    {/* Heading */}
                    <div className="mb-5">
                        <h1 className="text-[22px] font-bold text-[#e8f0fe] tracking-[-0.4px] leading-tight">Welcome back</h1>
                        <p className="text-[13px] text-[#7a99bb] mt-1">Continue building your developer skills</p>
                    </div>

                    {/* Fields */}
                    <div className="flex flex-col gap-3.5 mb-[18px]">
                        <Field label="Email" type="email" placeholder="name@gmail.com" value={email} onChange={setEmail} error={errors.email} />
                        <Field label="Password" type="password" placeholder="Your password" value={password} onChange={setPassword} error={errors.password} />
                    </div>

                    {/* Forgot */}
                    <div className="flex justify-end -mt-2 mb-4">
                        <button className="bg-transparent border-none text-[#7a99bb] text-xs cursor-pointer p-0 font-[inherit] hover:text-[#00e5c0] transition-colors">
                            Forgot password?
                        </button>
                    </div>

                    {/* CTA */}
                    <button
                        onClick={handleSignIn}
                        className="w-full py-[13px] bg-[#00e5c0] border-none rounded-lg text-[#001a14] text-[15px] font-bold cursor-pointer tracking-[0.1px] font-[inherit] transition-all duration-150 hover:bg-[#00b89a] active:scale-[0.99]"
                    >
                        Sign In
                    </button>

                    {/* Divider */}
                    <div className="flex items-center gap-2.5 my-4">
                        <div className="flex-1 h-px bg-[#1e3254]" />
                        {/*<span className="text-[11px] text-[#4a6380] uppercase tracking-[0.6px] whitespace-nowrap">or sign in with</span>*/}
                        <div className="flex-1 h-px bg-[#1e3254]" />
                    </div>

                    {/* Social */}
                    {/*<div className="flex gap-2.5">*/}
                    {/*    <button className="flex-1 flex items-center justify-center gap-2 py-2.5 bg-[#0a1525] border border-[#1e3254] rounded-[7px] text-[#7a99bb] text-[13px] font-medium cursor-pointer font-[inherit] transition-all hover:border-[#4a6380] hover:text-[#e8f0fe]">*/}
                    {/*        <GoogleIcon /> Google*/}
                    {/*    </button>*/}
                    {/*    <button className="flex-1 flex items-center justify-center gap-2 py-2.5 bg-[#0a1525] border border-[#1e3254] rounded-[7px] text-[#7a99bb] text-[13px] font-medium cursor-pointer font-[inherit] transition-all hover:border-[#4a6380] hover:text-[#e8f0fe]">*/}
                    {/*        <GitHubIcon /> GitHub*/}
                    {/*    </button>*/}
                    {/*</div>*/}

                    {/* Switch */}
                    <p className="mt-3.5 text-xs text-[#4a6380] text-center">
                        No account yet?{' '}
                        <Link to="/register" className="text-[#00e5c0] underline underline-offset-2 text-xs hover:text-[#00b89a]">
                            Create one
                        </Link>
                    </p>
                </div>

            </div>
        </div>
    )
}