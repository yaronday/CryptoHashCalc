import os
import re

def replace_chc_path_in_regfile(input_reg: str, chc_var: str = 'CHC_PATH') -> str:
    chc_value = os.environ.get(chc_var)
    if not chc_value:
        raise ValueError(f'Environment variable {chc_var} is not set.')
    chc_escaped = chc_value.replace('\\', '\\\\')

    pattern = r'(@="\\")%{}%\\\\CryptoHashCalc\.exe\\\"'.format(chc_var)

    def repl(match):
        return f'{match.group(1)}{chc_escaped}\\\\CryptoHashCalc.exe\\"'

    output = re.sub(pattern, repl, input_reg, flags=re.IGNORECASE)
    return output


def main():
	with open('CHCregCfgBase.reg', 'r', encoding='utf-8') as f:
		contents = f.read()
	updated_contents = replace_chc_path_in_regfile(contents, 'CHC_PATH')
	with open('CHCregCfgReady.reg', 'w', encoding='utf-8') as f:
		f.write(updated_contents)
	print('Registry file updated with actual path.')


if __name__ == '__main__':
	main()
