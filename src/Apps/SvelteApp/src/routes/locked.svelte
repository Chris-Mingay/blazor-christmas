<script>
	import Authguard from '$lib/Authguard.svelte';
	import Redirect from '$lib/Redirect.svelte';
</script>

<Authguard>
	<svelte:fragment slot='authed'>
		<div class='bg-yellow-500 rounded-2xl overflow-hidden p-4 text-white w-3/4 lg:w-1/2 mx-auto shadow-xl'>
			<h1 class='text-4xl font-bold mb-4'>Unlocked content</h1>
				<p class='text-lg mb-4 text-yellow-100'>You should not be able to see this without authentication</p>
				<div class='text-right'>
					<svg xmlns='http://www.w3.org/2000/svg' class='h-10 w-10 inline' fill='none' viewBox='0 0 24 24'
							 stroke='currentColor'>
						<path stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M13 5l7 7-7 7M5 5l7 7-7 7' />
					</svg>
				</div>
			</div>
	</svelte:fragment>
	<svelte:fragment slot='not_authed'>
		<Redirect url='/login'/>
	</svelte:fragment>
</Authguard>
